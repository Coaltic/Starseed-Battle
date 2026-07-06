using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] activeEnemies;
    public GameObject[] takenLocations;
    public int enemyIDNumber;
    public int enemySpawnNumber;

    public MoveableTileManager tileManager;

    private void Awake()
    {
        tileManager = this.GetComponent<MoveableTileManager>();
    }

    void Start()
    {
        enemySpawnNumber = Random.Range(0, tileManager.enemyTiles.Length);

        activeEnemies = new GameObject[(enemySpawnNumber)];


        for (int i = 0; i < enemySpawnNumber; i++)
        {
            enemyIDNumber = Random.Range(0, enemyPrefabs.Length);
            Debug.Log("Loading Enemy:" + enemyPrefabs[enemyIDNumber].name);
            activeEnemies[i] = Instantiate(enemyPrefabs[enemyIDNumber]);

            tileManager.InitialEnemySpawn(activeEnemies[i].GetComponent<Enemy>());
            SpawnEnemy(activeEnemies[i].GetComponent<Enemy>());

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy(Enemy enemy)
    {
        enemy.transform.position = enemy.currentLocationTile.transform.position;
    }
}
