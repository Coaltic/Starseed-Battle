using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameplayMenuManager : MonoBehaviour
{
    public MenuState menuState;
    public GameObject startButton;
    public Button[] gameplayButtons;
    public GameObject gameplayMenu;
    public EventSystem eventSystem;

    public GameObject BattleManager;
    // public TMP_Text startButtonText;
    // public GameObject _GameplayMenuManager;

    private void Awake()
    {
        int gameplayButtonCount = gameplayMenu.gameObject.transform.childCount;

        gameplayButtons = new Button[gameplayButtonCount];

        for (int i = 0; i < gameplayButtonCount; i++)
        {
            gameplayButtons[i] = gameplayMenu.gameObject.transform.GetChild(i).gameObject.GetComponent<Button>();
        }

        //eventSystem.firstSelectedGameObject = gameplayButtons[0].gameObject;
    }
    void Start()
    {
        
        SwitchState(MenuState.Start);
    }

    // Update is called once per frame
    void Update()
    {
        switch (menuState)
        {

            case MenuState.NotYourTurn:
                Debug.Log("State: Not your turn");

                break;

            case MenuState.Main:
                // Debug.Log("State: Main");
                // if (eventSystem.currentSelectedGameObject == null) eventSystem.firstSelectedGameObject = gameplayButtons[0].gameObject;
                break;

            case MenuState.Attack:
                Debug.Log("State: Attack");

                break;

            case MenuState.Magic:
                Debug.Log("State: Magic");

                break;

            case MenuState.Item:
                Debug.Log("State: Item");

                break;

            case MenuState.Defend:
                Debug.Log("State: Defend");

                break;

            case MenuState.SwapCharacter:
                Debug.Log("State: Swap Character");

                break;

            case MenuState.RunAway:
                Debug.Log("State: Run Away");

                break;
        }
    }

    void SwitchState(MenuState newState)
    {
        menuState = newState;
    }

    public void OnButtonClicked(string btnName)
    {
        Debug.Log(btnName + " Button: Clicked");
    }

    public void OnClickStart()
    {
        Destroy(startButton);
        Instantiate(BattleManager);
        eventSystem.firstSelectedGameObject = gameplayButtons[0].gameObject;
        eventSystem.SetSelectedGameObject(gameplayButtons[0].gameObject);
        gameplayMenu.gameObject.SetActive(true);
        SwitchState(MenuState.Main);
        //eventSystem.firstSelectedGameObject = gameplayButtons[0].gameObject;
        
    }
}

public enum MenuState
{
    Start,
    NotYourTurn,
    Main,
    Attack,
    Magic,
    Item,
    Defend,
    SwapCharacter,
    RunAway
}