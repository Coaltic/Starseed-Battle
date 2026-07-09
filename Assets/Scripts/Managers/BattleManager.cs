using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BattleManager : MonoBehaviour
{
    // temp variables
    public TMP_Text turnNumberText;


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

    public MoveableTileManager _tileManager;
    public GameplayMenuManager _gameplayMenuManager;

    //private Random rng = new Random();

    private void Awake()
    {
        turnNumberText = GameObject.Find("Turn Number Text").gameObject.GetComponent<TMP_Text>();
        _tileManager = this.GetComponent<MoveableTileManager>();
        _gameplayMenuManager = GameObject.Find("GameplayMenuManager").gameObject.GetComponent<GameplayMenuManager>();
    }

    void Start()
    {
        InitializeBattleCharacters();
        SetBattleOrder();

        

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurn[currentTurnNumber].tag == "Player")
        {

        }
        if (currentTurn[currentTurnNumber].tag == "Enemy")
        {
            currentTurn[currentTurnNumber].GetComponent<Enemy>().PickAttack(activePlayers, this);
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

        
    }


    public void PhysicalAttack(GameObject target)
    {
        target.gameObject.GetComponent<Character>().health += -(turnOrderList[currentTurnNumber].GetComponent<Character>().strengthStat);
        GameObject health = Instantiate(overheadHealthPrefab);
        //health.transform.parent = GameObject.Find("HUD Canvas").gameObject.transform;
        health.gameObject.transform.SetParent(GameObject.Find("HUD Canvas").gameObject.transform, false);
        health.transform.localScale = new Vector3(1, 1, 1);
        health.GetComponent<HealthChangeUI>().SetText(-turnOrderList[currentTurnNumber].GetComponent<Character>().strengthStat);
        // float offset = target.GetComponent<SpriteRenderer>().size.y;
        health.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1.0f, target.transform.position.z);
        // Debug.Log("Enemy Height: " + offset);

        target.gameObject.GetComponent<Character>().KnockBackEffect(target);

        EndOfTurn();
    }
    
    public void EndOfTurn()
    {
        if (currentTurnNumber == currentTurn.Length - 1) currentTurnNumber = 0;
        else currentTurnNumber++;
    }



}

public enum BattleState
{
    InMenu,
    EnemyAttacking,
    PlayerAttacking,
}