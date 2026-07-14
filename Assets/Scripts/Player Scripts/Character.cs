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
    public Animator anim;

    /*public float knockbackTimer = 0.5f;
    public float knockbackTimerMax = 0.5f;
    public bool isTakingKnockback;

    public Vector2 knockbackStartLocation;*/

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
        /*Debug.Log("Runnning");

        if (isTakingKnockback == true)
        {
            KnockBackEffect();
        }
        if (isTakingKnockback == false)
        {
            knockbackStartLocation = this.gameObject.transform.position;
        }*/
    }

    public void StartKnockBackEffect()
    {
        anim.SetBool("Knockback", true);
        // Debug.Log("Should have knockback");
    }

    public void EndKnockBackEffect()
    {
        anim.SetBool("Knockback", false);
    }
}
