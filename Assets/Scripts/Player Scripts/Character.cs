using UnityEngine;

public class Character : MonoBehaviour
{

    public int health;
    public int mp;
    public int strengthStat;
    public int magicStat;
    public int physicalDefenseStat;
    public int magicDefenseStat;
    public int speedStat;
    public int luckStat;
    public int movementStat;
    public int evasionStat;

    public bool myTurn;

    public MoveableTile currentLocationTile;
    public int turnOrder;
    public bool selectedForAttack;
    public string characterName;

    public Vector2 standardScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        standardScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockBackEffect(GameObject target)
    {

    }
}
