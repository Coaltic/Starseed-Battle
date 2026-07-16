using UnityEngine;

public class Player : Character
{
    public CharacterInfoPanel infoPanel;

    public void UpdateInfoBars()
    {
        if (health < 0) health = 0;
        infoPanel.charName.text = this.characterName;
        infoPanel.hpText.text = ("HP: " + this.health + "/" + this.maxHealth);
        infoPanel.mpText.text = ("MP: " + this.mp + "/" + this.maxMP);

        infoPanel.hpBar.transform.localScale = new Vector2(((float)health / (float)maxHealth), infoPanel.hpBar.transform.localScale.y);
        infoPanel.mpBar.transform.localScale = new Vector2(((float)mp / (float)maxMP), infoPanel.mpBar.transform.localScale.y);
        if (((float)health / (float)maxHealth) < 0.5f && ((float)health / (float)maxHealth) > 0.25f) infoPanel.hpBar.color = Color.orange;
        if (((float)health / (float)maxHealth) < 0.25f) infoPanel.hpBar.color = Color.red;
    }
}
