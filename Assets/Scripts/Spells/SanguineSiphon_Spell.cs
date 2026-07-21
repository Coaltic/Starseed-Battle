using UnityEngine;

public class SanguineSiphon_Spell : Spell
{
    public void Awake()
    {
        spellName = "Sanguine Siphon";
        spellMPCost = 5;
        doesRequireTarget = true;
    }

    public override void SpellSelected(Character currentTurnChar, Character target)
    {
        BattleManager _battleManager = GameObject.Find("BattleManager(Clone)").GetComponent<BattleManager>();
        int damageDelt = currentTurnChar.CalculateDamage(target);
        target.health += -(damageDelt);
        target.StartKnockBackEffect();
        GameObject health = Instantiate(_battleManager.overheadHealthPrefab);
        health.gameObject.transform.SetParent(GameObject.Find("HUD Canvas").gameObject.transform, false);
        health.transform.localScale = new Vector3(1, 1, 1);
        health.GetComponent<DamageEffect>().SetText(-damageDelt);
        health.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1.0f, target.transform.position.z);

        currentTurnChar.health += damageDelt;
        if (currentTurnChar.health > currentTurnChar.maxHealth) currentTurnChar.health = currentTurnChar.maxHealth;
        currentTurnChar.mp -= spellMPCost;

        _battleManager.turnActionText.text = currentTurnChar.name + " Attacked " + target.name;
        _battleManager.EndOfTurn();
    }
}
