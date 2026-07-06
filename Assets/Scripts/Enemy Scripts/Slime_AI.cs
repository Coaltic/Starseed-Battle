using UnityEngine;

public class Slime_AI : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 10;
        mp = 10;
        strengthStat = 20;
        magicStat = 20;
        physicalDefenseStat = 10;
        magicDefenseStat = 10;
        speedStat = 5;
        luckStat = 2;
        movementStat = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (myTurn)
        {

        }
    }
}
