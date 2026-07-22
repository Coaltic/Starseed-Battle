using UnityEngine;

public class FullHeal_Spell : Spell
{
    public void Awake()
    {
        spellName = "Full Heal";
        spellMPCost = 4;
        doesRequireTarget = false;

        _battleManager = GameObject.Find("BattleManager(Clone)").GetComponent<BattleManager>();
    }

    public override void SpellSelected(Character character)
    {
        character.HealHealth(character.maxHealth - character.health);
        character.mp -= spellMPCost;

        _battleManager.EndOfTurn();
    }
    
}
