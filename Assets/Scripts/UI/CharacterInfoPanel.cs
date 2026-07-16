using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoPanel : MonoBehaviour
{
    public TMP_Text charName;
    public TMP_Text hpText;
    public Image hpBar;
    public TMP_Text mpText;
    public Image mpBar;

    private void Awake()
    {
        charName = this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        hpText = this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        hpBar = hpText.gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
        mpText = this.gameObject.transform.GetChild(2).GetComponent<TMP_Text>();
        mpBar = mpText.gameObject.transform.GetChild(0).GetComponentInChildren<Image>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
