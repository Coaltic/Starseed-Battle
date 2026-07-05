using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    // public GameObject[] spawnLocations;
    public List<GameObject> spawnLocations;
    public GameObject[] activeEnemies;
    public GameObject[] takenLocations;
    public int enemyIDNumber;
    public int enemySpawnNumber;

    public int ranNum;

   
    void Awake()
    {
        enemySpawnNumber = Random.Range(0, spawnLocations.Count);
        // enemySpawnNumber = Random.Range(0, spawnLocations.Length);
        enemyIDNumber = Random.Range(0, enemyPrefabs.Length);
        activeEnemies = new GameObject[(enemySpawnNumber)];
        // activeEnemies[] = new GameObject[(enemySpawnNumber - 1);


        for (int i = 0; i < enemySpawnNumber; i++)
        {
            activeEnemies[i] = Instantiate(enemyPrefabs[0]);

            ranNum = Random.Range(0, spawnLocations.Count);
            activeEnemies[i].transform.position = spawnLocations[ranNum].transform.position;
            SpawnLocationUpdater();

            //takenLocations[i] = spawnLocations
        }

        //GameObject enemy1 = Instantiate(enemyPrefabs[0]);
        //enemy1.transform.position = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void TileAvailabilityCheck()
    {
        int newRanNum = Random.Range(0, spawnLocations.Count);
        
        spawnLocations.Remove(spawnLocations[ranNum]);

    }

    void SpawnLocationUpdater()
    {
        spawnLocations.Remove(spawnLocations[ranNum]);
    }
}
