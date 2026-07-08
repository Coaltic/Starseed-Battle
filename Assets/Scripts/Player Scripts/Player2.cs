using UnityEngine;

public class Player2 : Player
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 150;
        mp = 10;
        strengthStat = 10;
        magicStat = 25;
        physicalDefenseStat = 15;
        magicDefenseStat = 25;
        speedStat = 20;
        luckStat = 100;
        movementStat = 1;
        evasionStat = 10;

        myTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
