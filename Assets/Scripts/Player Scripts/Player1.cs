using UnityEngine;

public class Player1 : Player
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
        mp = 10;
        strengthStat = 20;
        magicStat = 10;
        physicalDefenseStat = 20;
        magicDefenseStat = 10;
        speedStat = 15;
        luckStat = 100;
        movementStat = 1;
        evasionStat = 5;

        myTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
