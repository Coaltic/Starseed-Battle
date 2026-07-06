using UnityEngine;
using UnityEngine.UI;

public class GameplayMenuManager : MonoBehaviour
{
    public MenuState menuState;
    public Button[] gameplayButtons;

    void Start()
    {
        
        SwitchState(MenuState.Main);
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
                Debug.Log("State: Main");

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
}

public enum MenuState
{
    NotYourTurn,
    Main,
    Attack,
    Magic,
    Item,
    Defend,
    SwapCharacter,
    RunAway
}