using UnityEngine;

[System.Serializable]
public class Spell : MonoBehaviour
{
    public string spellName;
    public string spellDescription;
    public int spellDuration;
    public int spellMPCost;


    public virtual void SpellSelected()
    {

    }

}
