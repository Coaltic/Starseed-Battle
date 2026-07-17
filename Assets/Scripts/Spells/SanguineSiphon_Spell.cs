using UnityEngine;

public class SanguineSiphon_Spell : Spell
{
    public void Awake()
    {
        spellName = "Sanguine Siphon";
        spellMPCost = 5;
    }

    public override void SpellSelected()
    {
        BattleManager _battleManager = GameObject.Find("BattleManager(Clone)").GetComponent<BattleManager>();
    }
}
