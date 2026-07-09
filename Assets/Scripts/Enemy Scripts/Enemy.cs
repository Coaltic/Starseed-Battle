using UnityEngine;

public class Enemy : Character
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        _battleManager.PhysicalAttack(target[rndAttackTarget].gameObject);

        Debug.Log(this.characterName + " Attacked " + target[rndAttackTarget].name);
        // target[Random.Range(0, target.Length)].GetComponent<Character>();
    }

    public void MagicAttackPlayer(GameObject[] target, BattleManager _battleManager)
    {
        // target[Random.Range(0, target.Length)].GetComponent<Character>();
    }
}
