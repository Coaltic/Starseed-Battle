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
    public GameObject characterInfoPanelPrefab;
    public CharacterInfoPanel[] infoPanels;
    public bool isMenuDisabled;

    public List<GameObject> previousMenuScreensList = new List<GameObject>();

    public GameObject BattleManager;
    public BattleManager _battleManager;
    

    private void Awake()
    {
        playerInventory = GameObject.Find("GameManager").gameObject.GetComponent<PlayerInventory>();
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
    public void OnClickStart(GameObject button)
    {
        _battleManager = Instantiate(BattleManager).GetComponent<BattleManager>();

        ChangeMenuScreen(gameplayMenus[0], null);
        
        
        Destroy(button);
    }

    public void SetInfoPanels()
    {
        GameObject charInfoPanelPrefab = Instantiate(characterInfoPanelPrefab);
        charInfoPanelPrefab.gameObject.transform.SetParent(GameObject.Find("Char Info HUD").gameObject.transform, false);
        infoPanels = new CharacterInfoPanel[charInfoPanelPrefab.gameObject.transform.childCount];
        for (int i = 0; i < charInfoPanelPrefab.gameObject.transform.childCount; i++)
        {
            infoPanels[i] = charInfoPanelPrefab.gameObject.transform.GetChild(i).gameObject.GetComponent<CharacterInfoPanel>();
            if (i < _battleManager.activePlayers.Length)
            {
                _battleManager.activePlayers[i].GetComponent<Player>().infoPanel = infoPanels[i];
                infoPanels[i].gameObject.SetActive(true);

            }
            else
            {
                infoPanels[i].gameObject.SetActive(false);
            }
        }
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
        SwitchState(MenuState.Attack);
        // MakeBackButton(gameplayMenus[1]);
        for (int i = 0; i < gameplayMenus[1].gameObject.transform.childCount; i++)
        {
            if (i == 0)
            {
                MakeBackButton(gameplayMenus[1]);
            }
            else if (i <= _battleManager.activeEnemies.Length)
            {
                Button btn = gameplayMenus[1].gameObject.transform.GetChild(i).GetComponent<Button>();
                gameplayMenus[1].gameObject.transform.GetChild(i).gameObject.SetActive(true);
                gameplayMenus[1].gameObject.transform.GetChild(i).gameObject.transform.GetComponentInChildren<TMP_Text>().text = _battleManager.activeEnemies[i - 1].name;
                GameObject target = _battleManager.activeEnemies[i - 1];
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(delegate { SetEnemyAttackButton(target); });


            }
            else
            {
                gameplayMenus[1].gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        ChangeMenuScreen(gameplayMenus[1], gameplayMenus[0]);
    }

    public void OnMagicClick()
    {
        Character currentTurnChar = _battleManager.currentTurn[_battleManager.currentTurnNumber];
        for (int i = 0; i < gameplayMenus[2].gameObject.transform.childCount; i++)
        {
            if (i == 0)
            {
                MakeBackButton(gameplayMenus[2]);
            }
            else if (i <= currentTurnChar.knownSpellsComponents.Length && currentTurnChar.knownSpellsComponents[i - 1] != null)
            {
                Debug.Log("not null");
                Button btn = gameplayMenus[2].gameObject.transform.GetChild(i).GetComponent<Button>();
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.SetActive(true);


                
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.transform.GetComponentInChildren<TMP_Text>().text = currentTurnChar.knownSpellsComponents[i - 1].spellName;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(delegate { CastSpell(currentTurnChar, i) ; });
                Debug.Log("i = " + i);


            }
            else
            {
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        ChangeMenuScreen(gameplayMenus[2], gameplayMenus[0]);
    }

    public void SetEnemyAttackButton(GameObject target)
    {
        _battleManager.PhysicalAttack(target);
        OnClickBack();
    }

    public void CastSpell(Character currentTurnChar, int spellNum)
    {
        Debug.Log("spellNum = " + spellNum);
        currentTurnChar.knownSpellsComponents[0].SpellSelected();
        OnClickBack();
    }

    public void OnItemClick()
    {
        SwitchState(MenuState.Item);
        // MakeBackButton(gameplayMenus[3]);
        for (int i = 0; i < playerInventory.inventoryItems.Count; i++)
        {
            if (i == 0)
            {
                MakeBackButton(gameplayMenus[3]);
            }
            else if (playerInventory.inventoryItems[i] != null)
            {

                gameplayMenus[3].gameObject.transform.GetChild(i).gameObject.SetActive(true);
                gameplayMenus[3].gameObject.transform.GetChild(i).gameObject.transform.GetComponentInChildren<TMP_Text>().text = playerInventory.inventoryItems[i];
                
            }
            else
            {
                // gameplayMenus[3].gameObject.SetActive(false);
            }
        }
        ChangeMenuScreen(gameplayMenus[3], gameplayMenus[0]);
    }

    

    public void ChangeMenuScreenBack(GameObject newMenuScreen, GameObject previousMenuScreen)
    {
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
        // Debug.Log("Runnning");
        eventSystem.SetSelectedGameObject(activeMenuButtons[0].gameObject);
    }

    public void EnableActiveMenuButtons()
    {
        if (isMenuDisabled == true)
        {
            for (int i = 0; i < gameplayMenus[0].gameObject.transform.childCount; i++)
            {
                if (gameplayMenus[0].gameObject.transform.GetChild(i).gameObject.GetComponent<Button>() != null)
                {
                    gameplayMenus[0].gameObject.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
                    activeMenuButtons[i] = gameplayMenus[0].gameObject.transform.GetChild(i).gameObject.GetComponent<Button>();
                }
            }

            eventSystem.SetSelectedGameObject(activeMenuButtons[0].gameObject);
            isMenuDisabled = false;
            
        }
        
    }

    public void DisableActiveMenuButtons()
    {
        if (isMenuDisabled == false)
        {
            for (int i = 0; i < gameplayMenus[0].gameObject.transform.childCount; i++)
            {
                if (gameplayMenus[0].gameObject.transform.GetChild(i).gameObject.GetComponent<Button>() != null) gameplayMenus[0].gameObject.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            }

            isMenuDisabled = true;
        }
        
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