using UnityEngine;

public class Player : Character
{
    public CharacterInfoPanel infoPanel;

    public int EXP;

    private void Start()
    {
    }

    public void UpdateInfoBars()
    {
        if (health < 0) health = 0;
        infoPanel.charName.text = this.characterName;
        infoPanel.hpText.text = ("HP: " + this.health + "/" + this.maxHealth);
        infoPanel.mpText.text = ("MP: " + this.mp + "/" + this.maxMP);

        infoPanel.hpBarColour.transform.localScale = new Vector2(((float)health / (float)maxHealth), infoPanel.hpBarColour.transform.localScale.y);
        infoPanel.mpBarColour.transform.localScale = new Vector2(((float)mp / (float)maxMP), infoPanel.mpBarColour.transform.localScale.y);
        if (((float)health / (float)maxHealth) > 0.5f) infoPanel.hpBarColour.color = Color.green;
        if (((float)health / (float)maxHealth) < 0.5f && ((float)health / (float)maxHealth) > 0.25f) infoPanel.hpBarColour.color = Color.orange;
        if (((float)health / (float)maxHealth) < 0.25f) infoPanel.hpBarColour.color = Color.red;
    }
}
