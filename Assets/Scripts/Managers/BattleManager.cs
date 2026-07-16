using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BattleManager : MonoBehaviour
{
    // temp variables
    public TMP_Text turnNumberText;
    public TMP_Text turnActionText;

    public BattleState battleState;
    public GameObject[] enemyPrefabs;
    public GameObject[] playerPrefabs;
    public GameObject[] activePlayers;
    public GameObject[] activeEnemies;
    public GameObject overheadHealthPrefab;

    public List<GameObject> turnOrderList;
    public Character[] currentTurn;
    public int currentTurnNumber;

    public int enemyIDNumber;
    public int enemySpawnNumber;

    public float attackCooldownTimer = 3.0f;
    public float attackCooldownTimerMax = 3.0f;
    public bool isAttackCooldownActive;
    public bool isBattleActive;

    public MoveableTileManager _tileManager;
    public GameplayMenuManager _gameplayMenuManager;

    //private Random rng = new Random();

    private void Awake()
    {
        turnNumberText = GameObject.Find("Turn Number Text").gameObject.GetComponent<TMP_Text>();
        turnActionText = GameObject.Find("Turn Action Text").gameObject.GetComponent<TMP_Text>();
        _tileManager = this.GetComponent<MoveableTileManager>();
        _gameplayMenuManager = GameObject.Find("GameplayMenuManager").gameObject.GetComponent<GameplayMenuManager>();
        
    }

    void Start()
    {
        InitializeBattleCharacters();
        SetBattleOrder();
        _gameplayMenuManager.SetInfoPanels();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattleActive)
        {
            if (currentTurnNumber > currentTurn.Length) currentTurnNumber = 0;
            if (currentTurn[currentTurnNumber].tag == "Player" && isAttackCooldownActive == false)
            {
                _gameplayMenuManager.EnableActiveMenuButtons();
            }
            if (currentTurn[currentTurnNumber].tag == "Enemy")
            {
                _gameplayMenuManager.DisableActiveMenuButtons();
                if (isAttackCooldownActive == false)
                {
                    currentTurn[currentTurnNumber].GetComponent<Enemy>().PickAttack(activePlayers, this);
                }
            }

            if (isAttackCooldownActive)
            {
                AttackCooldownTimer();
            }

        }


    }

    public void InitializeBattleCharacters()
    {
        enemySpawnNumber = 4; // Random.Range(1, _tileManager.enemyTiles.Length + 1);

        activeEnemies = new GameObject[(enemySpawnNumber)];


        for (int i = 0; i < enemySpawnNumber; i++)
        {
            enemyIDNumber = Random.Range(0, enemyPrefabs.Length);
            // Debug.Log("Loading Enemy:" + enemyPrefabs[enemyIDNumber].name);

            activeEnemies[i] = Instantiate(enemyPrefabs[enemyIDNumber]);
            activeEnemies[i].name = activeEnemies[i].GetComponent<Enemy>().characterName + " " + i;

            _tileManager.SetEnemySpawnLocation(activeEnemies[i].GetComponent<Enemy>());
            SpawnEnemy(activeEnemies[i].GetComponent<Enemy>());

        }

        activePlayers = new GameObject[1];

        activePlayers[0] = Instantiate(playerPrefabs[0]);
        activePlayers[0].name = activePlayers[0].GetComponent<Character>().characterName;
        _tileManager.SetHeroSpawnLocation(activePlayers[0].GetComponent<Player>());
        SpawnHero(activePlayers[0].GetComponent<Player>());


    }

    public void SpawnEnemy(Enemy enemy)
    {
        enemy.transform.position = enemy.currentLocationTile.transform.position;
    }

    public void SpawnHero(Player hero)
    {
        hero.transform.position = hero.currentLocationTile.transform.position;
    }

    public void SetBattleOrder()
    {
        for (int i = 0; i < activeEnemies.Length; i++)
        {
            turnOrderList.Add(activeEnemies[i]);
        }

        for (int i = 0; i < activePlayers.Length; i++)
        {
            turnOrderList.Add(activePlayers[i]);
        }

        currentTurn = new Character[turnOrderList.Count];
        Shuffle(turnOrderList);
    }

    // code provided by jasonmarziani on github - Fisher-Yates meathod
    void Shuffle(List<GameObject> a)
    {
        // Loops through array
        for (int i = a.Count - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            GameObject temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }

        // Print
        for (int i = 0; i < a.Count; i++)
        {
            a[i].GetComponent<Character>().turnOrder = i;
            currentTurn[i] = a[i].GetComponent<Character>();
            Debug.Log("Turn #" + i + " " + a[i]);
        }

        isBattleActive = true;
    }


    public void PhysicalAttack(GameObject target)
    {
        target.gameObject.GetComponent<Character>().health += -(turnOrderList[currentTurnNumber].GetComponent<Character>().strengthStat);
        target.gameObject.GetComponent<Character>().StartKnockBackEffect();
        GameObject health = Instantiate(overheadHealthPrefab);
        health.gameObject.transform.SetParent(GameObject.Find("HUD Canvas").gameObject.transform, false);
        health.transform.localScale = new Vector3(1, 1, 1);
        health.GetComponent<DamageEffect>().SetText(-turnOrderList[currentTurnNumber].GetComponent<Character>().strengthStat);
        health.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1.0f, target.transform.position.z);



        turnActionText.text = turnOrderList[currentTurnNumber].name + " Attacked " + target.name;

        EndOfTurn(target.gameObject.GetComponent<Character>());
    }

    public void EndOfTurn(Character character)
    {
        isAttackCooldownActive = true;
        if (currentTurnNumber == currentTurn.Length - 1) currentTurnNumber = 0;
        else currentTurnNumber++;
        turnNumberText.text = currentTurnNumber.ToString();
        if (character.health <= 0) DeathOfCharacter(character);
    }

    public void DeathOfCharacter(Character character)
    {
        int tempPlayerIncrement = 0;
        int tempEnemyIncrement = 0;
        if (character.turnOrder < currentTurnNumber) currentTurnNumber--;
        character.gameObject.SetActive(false);
        turnOrderList.Remove(character.gameObject);
        if (character.tag == "Player") activePlayers = new GameObject[activePlayers.Length - 1];
        if (character.tag == "Enemy") activeEnemies = new GameObject[activeEnemies.Length - 1];
        currentTurn = new Character[turnOrderList.Count];

        for (int i = 0; i < turnOrderList.Count; i++)
        {
            if (turnOrderList[i].gameObject.tag == "Player")
            {
                activePlayers[tempPlayerIncrement] = turnOrderList[i];
                tempPlayerIncrement++;
            }
            if (turnOrderList[i].gameObject.tag == "Enemy")
            {
                activeEnemies[tempEnemyIncrement] = turnOrderList[i];
                tempEnemyIncrement++;
            }
            currentTurn[i] = turnOrderList[i].GetComponent<Character>();
            currentTurn[i].turnOrder = i;
            
        }

        if (activeEnemies.Length == 0) BattleWon();
    }
        
    public void BattleWon()
    {
        isBattleActive = false;
        turnActionText.text = "YOU WON";
    }

    public void AttackCooldownTimer()
    {
        attackCooldownTimer -= Time.deltaTime;
        if (attackCooldownTimer <= 0.0f)
        {
            attackCooldownTimer = attackCooldownTimerMax;
            isAttackCooldownActive = false;
        }
    }



}

public enum BattleState
{
    InMenu,
    EnemyAttacking,
    PlayerAttacking,
}