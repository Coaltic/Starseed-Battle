using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int health;
    public int maxMP;
    public int mp;
    public int strengthStat;
    public int magicStat;
    public int physicalDefenseStat;
    public int magicDefenseStat;
    public int speedStat;
    public int luckStat;
    public int movementStat;
    public int evasionStat;

    public bool myTurn;
    public bool isDefending;

    public MoveableTile currentLocationTile;
    public int turnOrder;
    public bool selectedForAttack;

    public Spell[] knownSpellsComponents;

    public Vector2 standardScale;
    public Animator anim;

    public SpellCatalogue _spellCatalogue;
    public int knownSpellCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        standardScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    public void OnceATurn()
    {
        if (this.mp < this.maxMP) mp++;
        if (isDefending) isDefending = false;
    }

    public int CalculateDamage(Character target)
    {
        int damage = Random.Range((strengthStat / 2), (strengthStat * 2));
        Debug.Log("Calculated damage = " + damage);
        if (target.isDefending == false) return damage;
        else return damage / 2;
    }

    public void LearnSpell(SpellList newSpell, Character character)
    {
        if (_spellCatalogue == null) _spellCatalogue = GameObject.Find("GameManager/Spell Catalogue").gameObject.GetComponent<SpellCatalogue>();
        foreach (SpellCatalogue.SpellConstruct spellConstruct in _spellCatalogue.listOfSpells)
        {
            if (spellConstruct.spellName == newSpell)
            {
                knownSpellsComponents[character.knownSpellCount] = (Spell)character.gameObject.AddComponent(spellConstruct.spellScript.GetType());


                character.knownSpellCount++;
            }
        }
    }

    public void StartKnockBackEffect()
    {
        anim.SetBool("Knockback", true);
    }

    public void EndKnockBackEffect()
    {
        anim.SetBool("Knockback", false);
    }
}
