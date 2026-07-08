using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class GameplayMenuManager : MonoBehaviour
{
    public MenuState menuState;
    public GameObject startButton;

    public GameObject[] gameplayMenus;
    public Button[] activeMenuButtons;
    public GameObject gameplayMenuHUD;
    public EventSystem eventSystem;
    public PlayerInventory playerInventory;

    public List<GameObject> previousMenuScreensList = new List<GameObject>();

    public GameObject BattleManager;
    public BattleManager _battleManager;
    

    private void Awake()
    {
        playerInventory = GameObject.Find("GameManager").gameObject.GetComponent<PlayerInventory>();
        int gameplayButtonCount = gameplayMenuHUD.gameObject.transform.childCount;

        /*activeMenuButtons = new Button[gameplayButtonCount];

        for (int i = 0; i < gameplayButtonCount; i++)
        {
            activeMenuButtons[i] = gameplayMenuHUD.gameObject.transform.GetChild(i).gameObject.GetComponent<Button>();
        }*/

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
                // Debug.Log("State: Not your turn");

                break;

            case MenuState.Main:
                // Debug.Log("State: Main");
                // if (eventSystem.currentSelectedGameObject == null) eventSystem.firstSelectedGameObject = gameplayButtons[0].gameObject;
                break;

            case MenuState.Attack:
                // Debug.Log("State: Attack");

                break;

            case MenuState.Magic:
               //  Debug.Log("State: Magic");

                break;

            case MenuState.Item:
                // Debug.Log("State: Item");

                break;

            case MenuState.Defend:
                // Debug.Log("State: Defend");

                break;

            case MenuState.SwapCharacter:
                // Debug.Log("State: Swap Character");

                break;

            case MenuState.RunAway:
                // Debug.Log("State: Run Away");

                break;
        }
    }

    void SwitchState(MenuState newState)
    {
        menuState = newState;
    }

    public void OnButtonClicked(string btnName)
    {
        Debug.Log(btnName + " Button Clicked");
    }

    public void MakeBackButton(GameObject menu)
    {
        menu.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        menu.gameObject.transform.GetChild(0).gameObject.transform.GetComponentInChildren<TMP_Text>().text = "BACK";
    }

    public void OnClickBack()
    {
        ChangeMenuScreenBack(previousMenuScreensList[previousMenuScreensList.Count - 2], previousMenuScreensList[previousMenuScreensList.Count - 1]);
    }


    public void OnAttackClick()
    {
        SwitchState(MenuState.Item);
        MakeBackButton(gameplayMenus[1]);
        for (int i = 0; i < _battleManager.activeEnemies.Length; i++)
        {

            if (_battleManager.turnOrderList[i] != null)
            {
                Button btn = gameplayMenus[1].gameObject.transform.GetChild(i + 1).GetComponent<Button>();
                gameplayMenus[1].gameObject.transform.GetChild(i + 1).gameObject.SetActive(true);
                gameplayMenus[1].gameObject.transform.GetChild(i + 1).gameObject.transform.GetComponentInChildren<TMP_Text>().text = _battleManager.activeEnemies[i].name;
                GameObject target = _battleManager.activeEnemies[i];
                btn.onClick.AddListener(delegate { SetEnemyAttackButton(target); });


            }
            else
            {
                // gameplayMenus[3].gameObject.SetActive(false);
            }
        }
        ChangeMenuScreen(gameplayMenus[1], gameplayMenus[0]);
    }

    public void SetEnemyAttackButton(GameObject target)
    {
        Debug.Log("You tried to attack: " + target.name);
        _battleManager.PhysicalAttack(target);
    }

    public void OnItemClick()
    {
        SwitchState(MenuState.Item);
        MakeBackButton(gameplayMenus[3]);
        for (int i = 0; i < playerInventory.inventoryItems.Count; i++)
        {
            
            if (playerInventory.inventoryItems[i] != null)
            {

                gameplayMenus[3].gameObject.transform.GetChild(i + 1).gameObject.SetActive(true);
                gameplayMenus[3].gameObject.transform.GetChild(i + 1).gameObject.transform.GetComponentInChildren<TMP_Text>().text = playerInventory.inventoryItems[i];
                
            }
            else
            {
                // gameplayMenus[3].gameObject.SetActive(false);
            }
        }
        ChangeMenuScreen(gameplayMenus[3], gameplayMenus[0]);
    }

    public void OnClickStart()
    {
        Destroy(startButton);
        ChangeMenuScreen(gameplayMenus[0], null);
        _battleManager =  Instantiate(BattleManager).GetComponent<BattleManager>();
        // eventSystem.firstSelectedGameObject = activeMenuButtons[0].gameObject;
        // eventSystem.SetSelectedGameObject(activeMenuButtons[0].gameObject);
        // gameplayMenuHUD.gameObject.SetActive(true);
        SwitchState(MenuState.Main);
        //eventSystem.firstSelectedGameObject = gameplayButtons[0].gameObject;
        
    }

    public void ChangeMenuScreenBack(GameObject newMenuScreen, GameObject previousMenuScreen)
    {
        // Debug.Log("Changing Menu Back");
        
        previousMenuScreen.SetActive(false);
        newMenuScreen.SetActive(true);
        previousMenuScreensList.Remove(previousMenuScreen);
        UpdateActiveMenuButtons(newMenuScreen);
    }
    public void ChangeMenuScreen(GameObject newMenuScreen, GameObject previousMenuScreen)
    {
        // Debug.Log("Changing Menu");
        if (previousMenuScreen != null)
        {
            previousMenuScreensList.Add(previousMenuScreen);
            previousMenuScreensList.Add(newMenuScreen);
            previousMenuScreen.SetActive(false);
        }
        newMenuScreen.SetActive(true);

        activeMenuButtons = new Button[newMenuScreen.gameObject.transform.childCount];

        UpdateActiveMenuButtons(newMenuScreen);


    }

    public void UpdateActiveMenuButtons(GameObject newMenuScreen)
    {
        for (int i = 0; i < newMenuScreen.gameObject.transform.childCount; i++)
        {
            activeMenuButtons[i] = newMenuScreen.gameObject.transform.GetChild(i).gameObject.GetComponent<Button>();
        }

        eventSystem.SetSelectedGameObject(activeMenuButtons[0].gameObject);
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