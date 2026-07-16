using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int health;
    public int maxMP;
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

    public Spell[] knownSpells;

    public Vector2 standardScale;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        standardScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnceATurn()
    {
        if (this.mp < this.maxMP) mp++;
    }

    public int CalculateDamage()
    {
        return (Random.Range((strengthStat / 2), strengthStat * 2));
    }

    public void StartKnockBackEffect()
    {
        anim.SetBool("Knockback", true);
    }

    public void EndKnockBackEffect()
    {
        anim.SetBool("Knockback", false);
    }
}
