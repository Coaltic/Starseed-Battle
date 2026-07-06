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

    // Stores all availble tiles for the battle
    public void GetTileLocations()
    {
        int enemyTileCount = GameObject.Find("Tile Locations/Enemy Tile Locations").gameObject.transform.childCount;

        enemyTiles = new MoveableTile[enemyTileCount];

        for (int i = 0; i < enemyTileCount; i++)
        {
            enemyTiles[i] = GameObject.Find("Tile Locations/Enemy Tile Locations").gameObject.transform.GetChild(i).gameObject.GetComponent<MoveableTile>();
        }
    }

    // Spawns wave of enemies for the battle
    public void InitialEnemySpawn(Enemy enemy)
    {
        for (int i = 0; i < enemyTiles.Length; i++)
        {

            int randomLocation = Random.Range(0, enemyTiles.Length);

            if (enemyTiles[randomLocation].taken == false)
            {
                enemy.currentLocationTile = enemyTiles[randomLocation];
                enemyTiles[randomLocation].taken = true;
                SetEnemyRenderLevel(enemy);
                i = enemyTiles.Length;
            }
        }
    }


    // Sets render layer and size for enemies, will find better solution later
    public void SetEnemyRenderLevel(Enemy enemy)
    {
        if (enemy.currentLocationTile == enemyTiles[0] || enemy.currentLocationTile == enemyTiles[1] || enemy.currentLocationTile == enemyTiles[2])
        {
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 1;
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x / 2, enemy.transform.localScale.y / 2);
        }
        if (enemy.currentLocationTile == enemyTiles[3] || enemy.currentLocationTile == enemyTiles[4] || enemy.currentLocationTile == enemyTiles[5])
        {
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 2;
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x / 1.5f, enemy.transform.localScale.y / 1.5f);
        }
        if (enemy.currentLocationTile == enemyTiles[6] || enemy.currentLocationTile == enemyTiles[7] || enemy.currentLocationTile == enemyTiles[8])
        {
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 3;
            //enemy.transform.localScale = new Vector3(enemy.transform.localScale.x / 2, enemy.transform.localScale.y / 2);

        }
    }

}
