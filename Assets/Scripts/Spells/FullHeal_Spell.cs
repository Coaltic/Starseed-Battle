using UnityEngine;

public class FullHeal_Spell : Spell
{
    private void Start()
    {
        spellName = "Full Heal";
        spellMPCost = 4;
    }

    public void SpellSelected(Character character)
    {
        character.health = character.maxHealth;
    }
    
}
