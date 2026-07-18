using UnityEngine;

public class FullHeal_Spell : Spell
{
    public void Awake()
    {
        spellName = "Full Heal";
        Debug.Log("Awake");
        spellMPCost = 4;
    }

    public override void SpellSelected(Character character)
    {
        // Debug.Log("Full Heal Spell");
        // Character character = this.gameObject.GetComponent<Character>();
        character.health = character.maxHealth;
        BattleManager _battleManager = GameObject.Find("BattleManager(Clone)").GetComponent<BattleManager>();
        _battleManager.EndOfTurn(character);
    }
    
}
