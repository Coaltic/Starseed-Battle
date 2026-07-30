using UnityEngine;

public class Enemy : Character
{
    
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Runnning");
    }

    public void PickAttack(GameObject[] target, BattleManager _battleManager)
    {
        int rndAttack = Random.Range(0, 1);

        if (rndAttack == 0) PhysicalAttackPlayer(target, _battleManager);
        if (rndAttack == 1) MagicAttackPlayer(target, _battleManager);
    }

    public void PhysicalAttackPlayer(GameObject[] target, BattleManager _battleManager)
    {
        int rndAttackTarget = Random.Range(0, target.Length);

        target[rndAttackTarget].GetComponent<Character>().TakeDamage(this);

        _battleManager.turnActionText.text = this.characterName + " Attacked " + target[rndAttackTarget].GetComponent<Character>().characterName;

        _battleManager.EndOfTurn();
    }

    public void MagicAttackPlayer(GameObject[] target, BattleManager _battleManager)
    {
        // target[Random.Range(0, target.Length)].GetComponent<Character>();
    }
}
