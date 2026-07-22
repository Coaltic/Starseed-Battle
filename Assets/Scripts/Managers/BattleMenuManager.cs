using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using TMPro;

public class BattleMenuManager : MonoBehaviour
{
    public MenuState previousMenuState;
    public MenuState currentMenuState;
    public GameObject startButton;

    public GameObject[] gameplayMenus;
    public Button[] activeMenuButtons;
    public GameObject gameplayMenuHUD;
    public EventSystem eventSystem;
    public PlayerInventory playerInventory;
    public GameObject characterInfoPanelPrefab;
    public GameObject[] enemyIndicationArrows;
    public GameObject currentlySelectedButton;
    public CharacterInfoPanel[] infoPanels;
    public bool isMenuDisabled;

    public List<GameObject> previousMenuScreensList = new List<GameObject>();
    public List<MenuState> menuStateList;

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
        switch (currentMenuState)
        {

            case MenuState.NotYourTurn:
                // Debug.Log("State: Not your turn");

                break;

            case MenuState.Main:
                ClearIndicationArrows();
                break;

            case MenuState.Attack:
                UpdateIndicationArrows();

                break;

            case MenuState.Magic:

                break;

            case MenuState.PickingTarget:
                UpdateIndicationArrows();

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

    public void SwitchState(MenuState newState)
    {
        currentMenuState = newState;
        menuStateList.Add(currentMenuState);
    }

    public void SwitchStateBack()
    {
        currentMenuState = menuStateList[menuStateList.Count - 1];
    }
    public void OnClickStart(GameObject button)
    {
        _battleManager = Instantiate(BattleManager).GetComponent<BattleManager>();
        SwitchState(MenuState.Main);
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

    public void SetIndicationArrows()
    {
        enemyIndicationArrows = new GameObject[_battleManager.activeEnemies.Length];
        for (int i = 0; i < enemyIndicationArrows.Length; i++)
        {
            enemyIndicationArrows[i] = _battleManager.activeEnemies[i].transform.GetChild(1).gameObject;
        }
    }

    public void UpdateIndicationArrows()
    {
        currentlySelectedButton = EventSystem.current.currentSelectedGameObject;

        if (currentlySelectedButton.transform.GetSiblingIndex() - 1 >= 0)
        {
            enemyIndicationArrows[currentlySelectedButton.transform.GetSiblingIndex() - 1].gameObject.SetActive(true);  // _battleManager.activeEnemies[currentlySelectedButton.transform.GetSiblingIndex() - 1].transform.GetChild(1).gameObject.SetActive(true);

        }

        for (int i = 0; i < enemyIndicationArrows.Length; i++)
        {
            if (i != currentlySelectedButton.transform.GetSiblingIndex() - 1) enemyIndicationArrows[i].gameObject.SetActive(false);
        }
    }

    public void ClearIndicationArrows()
    {
        for (int i = 0; i < enemyIndicationArrows.Length; i++)
        {
            enemyIndicationArrows[i].gameObject.SetActive(false);
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
        SwitchState(MenuState.Magic);
        Character currentTurnChar = _battleManager.currentTurn[_battleManager.currentTurnNumber];
        for (int i = 0; i < gameplayMenus[2].gameObject.transform.childCount; i++)
        {
            if (i == 0)
            {
                MakeBackButton(gameplayMenus[2]);
            }
            else if (i <= currentTurnChar.knownSpellsComponents.Length && currentTurnChar.knownSpellsComponents[i - 1] != null)
            {
                Button btn = gameplayMenus[2].gameObject.transform.GetChild(i).GetComponent<Button>();
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.SetActive(true);


                
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.transform.GetComponentInChildren<TMP_Text>().text = currentTurnChar.knownSpellsComponents[i - 1].spellName;
                if (currentTurnChar.mp < currentTurnChar.knownSpellsComponents[i - 1].spellMPCost) btn.interactable = false;
                else btn.interactable = true;
                btn.onClick.RemoveAllListeners();

                int spellNum = i - 1;
                if (!currentTurnChar.knownSpellsComponents[i - 1].doesRequireTarget)
                {
                    btn.onClick.AddListener(delegate { CastSpell(currentTurnChar, spellNum); });
                }
                else
                {
                    btn.onClick.AddListener(delegate { SetEnemyMagicTarget(currentTurnChar, spellNum); });
                }


            }
            else
            {
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        ChangeMenuScreen(gameplayMenus[2], gameplayMenus[0]);
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

    public void OnDefendClick()
    {
        _battleManager.currentTurnChar.isDefending = true;
        _battleManager.EndOfTurn();
    }

    public void SetEnemyAttackButton(GameObject target)
    {
        _battleManager.PhysicalAttack(target);
        OnClickBack();
    }

    public void SetEnemyMagicTarget(Character currentTurnChar, int spellNum)
    {
        SwitchState(MenuState.PickingTarget);
        for (int i = 0; i < gameplayMenus[2].gameObject.transform.childCount; i++)
        {
            if (i == 0)
            {
                // MakeBackButton(gameplayMenus[2]);
            }
            else if (i <= _battleManager.activeEnemies.Length)
            {
                Button btn = gameplayMenus[2].gameObject.transform.GetChild(i).GetComponent<Button>();
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.SetActive(true);
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.transform.GetComponentInChildren<TMP_Text>().text = _battleManager.activeEnemies[i - 1].name;
                GameObject target = _battleManager.activeEnemies[i - 1];
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(delegate { CastSpell(currentTurnChar, spellNum, target.GetComponent<Character>()); });

            }
            else
            {
                gameplayMenus[2].gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        // ChangeMenuScreen(gameplayMenus[1], gameplayMenus[0]);
    }

    public void CastSpell(Character character, int spellNum)
    {

        // Debug.Log("btnNum = " + btnNum);
        character.knownSpellsComponents[spellNum].SpellSelected(character);
        OnClickBack();
    }

    public void CastSpell(Character character, int spellNum, Character target)
    {
        Debug.Log("CastSpell spellNum = " + spellNum);
        character.knownSpellsComponents[spellNum].SpellSelected(character, target);
        OnClickBack();
    }

    public void ChangeMenuScreenBack(GameObject newMenuScreen, GameObject previousMenuScreen)
    {
        menuStateList.Remove(menuStateList[menuStateList.Count - 1]);
        SwitchStateBack();
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
    PickingTarget,
    Item,
    Defend,
    SwapCharacter,
    RunAway
}