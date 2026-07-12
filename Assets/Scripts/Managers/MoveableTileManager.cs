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

        int playerTileCount = GameObject.Find("Tile Locations/Hero Tile Locations").gameObject.transform.childCount;

        playerTiles = new MoveableTile[playerTileCount];

        for (int i = 0; i < playerTileCount; i++)
        {
            playerTiles[i] = GameObject.Find("Tile Locations/Hero Tile Locations").gameObject.transform.GetChild(i).gameObject.GetComponent<MoveableTile>();
        }
    }

    // Spawns wave of enemies for the battle
    public void SetEnemySpawnLocation(Enemy enemy)
    {
        for (int i = 0; i < 100; i++)
        {

            int randomLocation = Random.Range(0, enemyTiles.Length);

            if (enemyTiles[randomLocation].taken == false)
            {
                enemy.currentLocationTile = enemyTiles[randomLocation];
                enemyTiles[randomLocation].taken = true;
                SetEnemyRenderLevel(enemy);
                break;
            }
            else
            {

            }
        }
    }

    public void SetHeroSpawnLocation(Player player)
    {
        for (int i = 0; i < 100; i++)
        {

            int randomLocation = Random.Range(0, playerTiles.Length);

            if (playerTiles[randomLocation].taken == false)
            {
                player.currentLocationTile = playerTiles[randomLocation];
                playerTiles[randomLocation].taken = true;
                SetHeroRenderLevel(player);
                break;
            }
        }
    }


    // Sets render layer and size for enemies, will find better solution later
    public void SetEnemyRenderLevel(Enemy enemy)
    {
        if (enemy.GetComponent<SpriteRenderer>() == null)
        {
            if (enemy.currentLocationTile == enemyTiles[0] || enemy.currentLocationTile == enemyTiles[1] || enemy.currentLocationTile == enemyTiles[2])
            {
                enemy.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x / 1.5f, enemy.transform.localScale.y / 1.5f);
            }
            if (enemy.currentLocationTile == enemyTiles[3] || enemy.currentLocationTile == enemyTiles[4] || enemy.currentLocationTile == enemyTiles[5])
            {
                enemy.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x / 1.25f, enemy.transform.localScale.y / 1.25f);
            }
            if (enemy.currentLocationTile == enemyTiles[6] || enemy.currentLocationTile == enemyTiles[7] || enemy.currentLocationTile == enemyTiles[8])
            {
                enemy.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
                // enemy.transform.localScale = enemy.standardScale;

            }
        }
        else
        {
            if (enemy.currentLocationTile == enemyTiles[0] || enemy.currentLocationTile == enemyTiles[1] || enemy.currentLocationTile == enemyTiles[2])
            {
                enemy.GetComponent<SpriteRenderer>().sortingOrder = 1;
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x / 1.5f, enemy.transform.localScale.y / 1.5f);
            }
            if (enemy.currentLocationTile == enemyTiles[3] || enemy.currentLocationTile == enemyTiles[4] || enemy.currentLocationTile == enemyTiles[5])
            {
                enemy.GetComponent<SpriteRenderer>().sortingOrder = 2;
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x / 1.25f, enemy.transform.localScale.y / 1.25f);
            }
            if (enemy.currentLocationTile == enemyTiles[6] || enemy.currentLocationTile == enemyTiles[7] || enemy.currentLocationTile == enemyTiles[8])
            {
                enemy.GetComponent<SpriteRenderer>().sortingOrder = 3;
                // enemy.transform.localScale = enemy.standardScale;

            }
        }
    }

    public void SetHeroRenderLevel(Player hero)
    {
        if (hero.currentLocationTile == playerTiles[0] || hero.currentLocationTile == playerTiles[1] || hero.currentLocationTile == playerTiles[2])
        {
            hero.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            hero.transform.localScale = new Vector3(hero.transform.localScale.x / 1.5f, hero.transform.localScale.y / 1.5f);
        }
        if (hero.currentLocationTile == playerTiles[3] || hero.currentLocationTile == playerTiles[4] || hero.currentLocationTile == playerTiles[5])
        {
            hero.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            hero.transform.localScale = new Vector3(hero.transform.localScale.x / 1.25f, hero.transform.localScale.y / 1.25f);
        }
        if (hero.currentLocationTile == playerTiles[6] || hero.currentLocationTile == playerTiles[7] || hero.currentLocationTile == playerTiles[8])
        {
            hero.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
            // hero.transform.localScale = hero.standardScale;

        }
    }

}
