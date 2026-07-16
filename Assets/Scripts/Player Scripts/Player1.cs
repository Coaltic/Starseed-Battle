using UnityEngine;

public class Player1 : Player
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        maxHealth = 200;
        maxMP = 10;
        strengthStat = 20;
        magicStat = 10;
        physicalDefenseStat = 20;
        magicDefenseStat = 10;
        speedStat = 15;
        luckStat = 100;
        movementStat = 1;
        evasionStat = 5;

        characterName = "Player 1";
        myTurn = false;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        mp = maxMP;
    }

    // Update is called once per frame
    public void Update()
    {
        if (this.infoPanel != null)
        {
            UpdateInfoBars();
        }
    }

    

}
