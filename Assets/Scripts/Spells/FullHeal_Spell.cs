using UnityEngine;

public class FullHeal_Spell : Spell
{
    public void Awake()
    {
        spellName = "Full Heal";
        spellMPCost = 4;
        doesRequireTarget = false;
    }

    public override void SpellSelected(Character character)
    {
        Debug.Log("Full Heal Spell");
        // Character character = this.gameObject.GetComponent<Character>();
        character.health = character.maxHealth;
        character.mp -= spellMPCost;
        BattleManager _battleManager = GameObject.Find("BattleManager(Clone)").GetComponent<BattleManager>();
        _battleManager.EndOfTurn();
    }
    
}
