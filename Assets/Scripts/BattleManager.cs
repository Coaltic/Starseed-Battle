using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnLocations;
    public GameObject[] activeEnemies;
    public GameObject[] takenLocations;
    public int enemySpawnNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Awake()
    {
        enemySpawnNumber = Random.Range(0, spawnLocations.Length);
        activeEnemies[] = new GameObject[(enemySpawnNumber - 1);
        

        for (int i = 0; i < enemySpawnNumber; i++)
        {
            activeEnemies[i] = Instantiate(enemyPrefabs[0]);


            activeEnemies[i].transform.position = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;

            //takenLocations[i] = spawnLocations
        }


        //GameObject enemy1 = Instantiate(enemyPrefabs[0]);
        //enemy1.transform.position = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
