using UnityEngine;
using System.Collections.Generic;

public class SpellCatalogue : MonoBehaviour
{
    public SpellConstruct[] listOfSpells;

    private void Start()
    {
        
    }

    [System.Serializable]
    public class SpellConstruct
    {
        public SpellList spellName;
        public Spell spellScript;
    }


}

public enum SpellList
{
    FullHeal,
    FireBall,
    SanguineSiphon,
    NumberOfSpells
}


