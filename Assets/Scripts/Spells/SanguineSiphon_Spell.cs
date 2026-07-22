using UnityEngine;

public class SanguineSiphon_Spell : Spell
{
    public void Awake()
    {
        spellName = "Sanguine Siphon";
        spellMPCost = 5;
        doesRequireTarget = true;

        _battleManager = GameObject.Find("BattleManager(Clone)").GetComponent<BattleManager>();
    }

    public override void SpellSelected(Character currentTurnChar, Character target)
    {
        int damageDelt = target.CalculateDamage(currentTurnChar);
        target.TakeDamage(damageDelt);
        currentTurnChar.HealHealth(damageDelt);
        currentTurnChar.mp -= spellMPCost;

        _battleManager.turnActionText.text = currentTurnChar.name + " Attacked " + target.name;
        _battleManager.EndOfTurn();
    }
}
