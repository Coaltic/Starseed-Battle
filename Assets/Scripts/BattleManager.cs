using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    // public GameObject[] spawnLocations;
    //public List<GameObject> spawnLocations;
    public GameObject[] activeEnemies;
    public GameObject[] takenLocations;
    public int enemyIDNumber;
    public int enemySpawnNumber;

    public int ranNum;

    public MoveableTileManager tileManager;

    private void Awake()
    {
        tileManager = this.GetComponent<MoveableTileManager>();
    }

    void Start()
    {
        enemySpawnNumber = Random.Range(0, tileManager.enemyTiles.Length);
        enemyIDNumber = Random.Range(0, enemyPrefabs.Length);
        activeEnemies = new GameObject[(enemySpawnNumber)];


        for (int i = 0; i < enemySpawnNumber; i++)
        {
            activeEnemies[i] = Instantiate(enemyPrefabs[0]);

            // ranNum = Random.Range(0, tileManager.enemyTiles.Length);

            // activeEnemies[i].GetComponent<Enemy>().currentLocationTile = this.gameObject.GetComponent<MoveableTileManager>().enemyTiles[ranNum];
            //tileManager.InitialEnemySpawn(activeEnemies[i].GetComponent<Enemy>());

            Transform spawnLocation = tileManager.InitialEnemySpawn(activeEnemies[i].GetComponent<Enemy>());
            SpawnEnemy(activeEnemies[i].GetComponent<Enemy>(), spawnLocation);
            //activeEnemies[i].transform.position = spawnLocations[this.GetComponent<MoveableTileManager>().InitialEnemySpawn(activeEnemies[i].GetComponent<Enemy>())].transform.position;
            // SpawnLocationUpdater();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(Enemy enemy, Transform enemyPosition)
    {
        enemy.transform.position = enemyPosition.position;
    }

    

    /*void TileAvailabilityCheck()
    {
        int newRanNum = Random.Range(0, spawnLocations.Count);
        
        spawnLocations.Remove(spawnLocations[ranNum]);

    }

    void SpawnLocationUpdater()
    {
        spawnLocations.Remove(spawnLocations[ranNum]);
    }*/
}
