using UnityEngine;

[System.Serializable]
public class Spell : MonoBehaviour
{
    public BattleManager _battleManager;

    public string spellName;
    public string spellDescription;
    public int spellDuration;
    public int spellMPCost;

    public bool doesRequireTarget;


    public virtual void SpellSelected()
    {

    }

    public virtual void SpellSelected(Character currentTurnChar)
    {

    }

    public virtual void SpellSelected(Character currentTurnChar, Character target)
    {

    }

}
