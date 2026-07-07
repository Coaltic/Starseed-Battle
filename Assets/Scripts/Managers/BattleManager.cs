using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public BattleState battleState;
    public GameObject[] enemyPrefabs;
    public GameObject[] playerPrefabs;
    public GameObject[] activePlayers;
    public GameObject[] activeEnemies;
    // public GameObject[] takenLocations;
    public int enemyIDNumber;
    public int enemySpawnNumber;

    public MoveableTileManager _tileManager;
    public GameplayMenuManager _gameplayMenuManager;

    private void Awake()
    {
        _tileManager = this.GetComponent<MoveableTileManager>();
        _gameplayMenuManager = GameObject.Find("GameplayMenuManager").gameObject.GetComponent<GameplayMenuManager>();
    }

    void Start()
    {
        enemySpawnNumber = Random.Range(1, _tileManager.enemyTiles.Length + 1);

        activeEnemies = new GameObject[(enemySpawnNumber)];


        for (int i = 0; i < enemySpawnNumber; i++)
        {
            enemyIDNumber = Random.Range(0, enemyPrefabs.Length);
            Debug.Log("Loading Enemy:" + enemyPrefabs[enemyIDNumber].name);
            activeEnemies[i] = Instantiate(enemyPrefabs[enemyIDNumber]);

            _tileManager.SetEnemySpawnLocation(activeEnemies[i].GetComponent<Enemy>());
            SpawnEnemy(activeEnemies[i].GetComponent<Enemy>());

        }

        activePlayers = new GameObject[1];
        activePlayers[0] = Instantiate(playerPrefabs[0]);
        _tileManager.SetHeroSpawnLocation(activePlayers[0].GetComponent<Player>());
        SpawnHero(activePlayers[0].GetComponent<Player>());
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

    public void SpawnEnemy(Enemy enemy)
    {
        enemy.transform.position = enemy.currentLocationTile.transform.position;
    }

    public void SpawnHero(Player hero)
    {
        hero.transform.position = hero.currentLocationTile.transform.position;
    }

}

public enum BattleState
{
    InMenu,
    EnemyAttacking,
    PlayerAttacking,
}