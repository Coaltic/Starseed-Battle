using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public BattleState battleState;
    public GameObject[] enemyPrefabs;
    public GameObject[] playerPrefabs;
    public GameObject[] activePlayers;
    public GameObject[] activeEnemies;

    public List<GameObject> turnOrderList;
    
    public int enemyIDNumber;
    public int enemySpawnNumber;

    public MoveableTileManager _tileManager;
    public GameplayMenuManager _gameplayMenuManager;

    //private Random rng = new Random();

    private void Awake()
    {
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
        switch (battleState)
        {

            case BattleState.InMenu:
                // Debug.Log("State: In Menu");

                break;

            case BattleState.PlayerAttacking:
                // Debug.Log("State: Player Attacking");

                break;

            case BattleState.EnemyAttacking:
                // Debug.Log("State: Enemy Attacking");

                break;
        }
    }

    public void InitializeBattleCharacters()
    {
        enemySpawnNumber = Random.Range(1, _tileManager.enemyTiles.Length + 1);

        activeEnemies = new GameObject[(enemySpawnNumber)];


        for (int i = 0; i < enemySpawnNumber; i++)
        {
            enemyIDNumber = Random.Range(0, enemyPrefabs.Length);
            // Debug.Log("Loading Enemy:" + enemyPrefabs[enemyIDNumber].name);
            activeEnemies[i] = Instantiate(enemyPrefabs[enemyIDNumber]);

            _tileManager.SetEnemySpawnLocation(activeEnemies[i].GetComponent<Enemy>());
            SpawnEnemy(activeEnemies[i].GetComponent<Enemy>());

        }

        activePlayers = new GameObject[1];
        activePlayers[0] = Instantiate(playerPrefabs[0]);
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
            Debug.Log("Turn #" + i + " " + a[i]);
        }
    }



}

public enum BattleState
{
    InMenu,
    EnemyAttacking,
    PlayerAttacking,
}