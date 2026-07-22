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
    public GameObject healthChangePrefab;

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

    public void TakeDamage(Character attacker)
    {
        int damageDelt = CalculateDamage(attacker);
        this.health += -(damageDelt);
        StartKnockBackEffect();
        GameObject healthObject = Instantiate(healthChangePrefab);
        healthObject.gameObject.transform.SetParent(GameObject.Find("HUD Canvas").gameObject.transform, false);
        healthObject.transform.localScale = new Vector3(1, 1, 1);
        healthObject.GetComponent<HealthChangePrefab>().SetText(-damageDelt);
        healthObject.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
    }

    public void TakeDamage(int damageDelt)
    {
        this.health += -(damageDelt);
        StartKnockBackEffect();
        GameObject healthObject = Instantiate(healthChangePrefab);
        healthObject.gameObject.transform.SetParent(GameObject.Find("HUD Canvas").gameObject.transform, false);
        healthObject.transform.localScale = new Vector3(1, 1, 1);
        healthObject.GetComponent<HealthChangePrefab>().SetText(-damageDelt);
        healthObject.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
    }

    public void HealHealth(int healthAmount)
    {
        this.health += healthAmount;
        if (this.health > this.maxHealth) this.health = this.maxHealth;

        GameObject healthObject = Instantiate(healthChangePrefab);
        healthObject.gameObject.transform.SetParent(GameObject.Find("HUD Canvas").gameObject.transform, false);
        healthObject.transform.localScale = new Vector3(1, 1, 1);
        healthObject.GetComponent<HealthChangePrefab>().SetText(healthAmount);
        healthObject.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

    }

    public int CalculateDamage(Character attacker)
    {
        int damage = Random.Range((attacker.strengthStat / 2), (attacker.strengthStat * 2));
        if (isDefending == false) return damage;
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
