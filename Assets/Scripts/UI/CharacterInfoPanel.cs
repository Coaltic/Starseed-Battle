using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoPanel : MonoBehaviour
{
    public TMP_Text charName;
    public TMP_Text hpText;
    public Image hpBarColour;
    public TMP_Text mpText;
    public Image mpBarColour;

    private void Awake()
    {
        charName = this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        hpText = this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        hpBarColour = hpText.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        mpText = this.gameObject.transform.GetChild(2).GetComponent<TMP_Text>();
        mpBarColour = mpText.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();

        RectTransform hpBarColourRect = hpBarColour.GetComponent<RectTransform>();
        RectTransform mpBarColourRect = mpBarColour.GetComponent<RectTransform>();

        hpBarColourRect.pivot = new Vector2(0, 0.5f);

        mpBarColourRect.pivot = new Vector2(0, 0.5f);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
