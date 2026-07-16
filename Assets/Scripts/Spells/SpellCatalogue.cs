using UnityEngine;
using System.Collections.Generic;

public class SpellCatalogue : MonoBehaviour
{
    public ExactSpell[] listOfSpells;

    private void Start()
    {
        
    }

    [System.Serializable]
    public class ExactSpell
    {
        public SpellList spell;
        public Spell spellScript;
    }


}

public enum SpellList
{
    FullHeal,
    FireBall
}


