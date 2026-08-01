using UnityEngine;

public class Catnolli_AI : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        maxHealth = 40;
        health = 40;
        mp = 10;
        strengthStat = 10;
        magicStat = 20;
        physicalDefenseStat = 10;
        magicDefenseStat = 10;
        speedStat = 5;
        luckStat = 2;
        movementStat = 1;

        characterName = "Catnolli";
        myTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myTurn)
        {

        }

        if (health <= 0) { anim.SetBool("Dead", true); anim.SetBool("Alive", false); }
    }

    public override void StartKnockBackEffect()
    {
        anim.SetTrigger("Take Damage");
    }

    public void EndAttackAnimation()
    {
        anim.SetBool("Attacking", false);
    }
}
