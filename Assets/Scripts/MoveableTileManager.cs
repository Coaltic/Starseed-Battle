using UnityEngine;

public class MoveableTileManager : MonoBehaviour
{

    public MoveableTile[] enemyTiles;
    public MoveableTile[] playerTiles;


    private void Awake()
    {
        GetTileLocations();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTileLocations()
    {
        int enemyTileCount = GameObject.Find("Tile Locations/Enemy Tile Locations").gameObject.transform.childCount;
        // Debug.Log("Enemy Tile Count: " + enemyTileCount);

        enemyTiles = new MoveableTile[enemyTileCount];

        for (int i = 0; i < enemyTileCount; i++)
        {
            enemyTiles[i] = GameObject.Find("Tile Locations/Enemy Tile Locations").gameObject.transform.GetChild(i).gameObject.GetComponent<MoveableTile>();
        }
    }

    public Transform InitialEnemySpawn(Enemy enemy)
    {
        

        for (int i = 0; i < enemyTiles.Length; i++)
        {

            int randomLocation = Random.Range(0, enemyTiles.Length);

            if (enemyTiles[randomLocation].taken == false)
            {
                enemy.currentLocationTile = enemyTiles[randomLocation];
                enemyTiles[randomLocation].taken = true;
                return enemyTiles[randomLocation].transform;
                // enemy.GetComponent<Enemy>().currentLocationTile = this.gameObject.GetComponent<MoveableTileManager>().enemyTiles[randomLocation];
            }
        }



        Debug.Log("No Location Found");

        return null;

        
    }

}
