using UnityEngine;
using TMPro;

public class StartButton : MonoBehaviour
{
    public GameObject BattleManager;
    public TMP_Text buttonText;
    public GameObject GameplayMenuManager;

    void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonText.text = "START";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void OnClickStart()
    {
        Instantiate(BattleManager);
        transform.parent.Find("Gameplay Menu").gameObject.SetActive(true);
        GameplayMenuManager.SetActive(true);
        Destroy(this.gameObject);
    }*/
}
