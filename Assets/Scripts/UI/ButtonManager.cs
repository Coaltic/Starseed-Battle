using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public Button[] buttonLocations;
    public MenuButtonConstruct[] currentButtonConstructs;
    public List<MenuButtonConstruct> listOfMenuButtons;
    public List<MenuButtonConstruct> listOfEnemyButtons;
    public List<MenuButtonConstruct> listOfMagicButtons;
    public EventSystem eventSystem;
    public GameObject buttonContainer;

    public MenuScreen currentMenuScreen;
    public ButtonType currentButtonType;
    public bool onceATurn;

    public GameObject startButton;

    public GameObject[] gameplayMenus;
    public PlayerInventory playerInventory;
    public GameObject characterInfoPanelPrefab;
    public GameObject[] enemyIndicationArrows;
    public GameObject currentlySelectedButton;
    public CharacterInfoPanel[] infoPanels;
    public bool isMenuDisabled;

    public List<GameObject> previousMenuScreensList = new List<GameObject>();
    public List<MenuScreen> menuScreenList;

    public GameObject battleManagerPrefab;
    public BattleManager _battleManager;

    private void Awake()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        playerInventory = GameObject.Find("GameManager").gameObject.GetComponent<PlayerInventory>();
        // SwitchState(MenuScreen.Start);
        
    }

    void Start()
    {
        eventSystem.SetSelectedGameObject(startButton);
    }

    public void MainMenuButtonCreation()
    {
        CreateNewButtons(MenuScreen.Main, ButtonType.Attack, "ATTACK");
        CreateNewButtons(MenuScreen.Main, ButtonType.Defend, "DEFEND");
        CreateNewButtons(MenuScreen.Main, ButtonType.Magic, "MAGIC");
        CreateNewButtons(MenuScreen.Main, ButtonType.Item, "ITEM");
        CreateNewButtons(MenuScreen.Main, ButtonType.Move, "MOVE");
        CreateNewButtons(MenuScreen.Main, ButtonType.Swap, "SWAP");
        CreateNewButtons(MenuScreen.Main, ButtonType.RunAway, "RUN AWAY");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentButtonType)
        {

            case ButtonType.Attack:
                OnAttackClick(currentButtonConstructs[0]);

                break;

            case ButtonType.Defend:
                OnButtonClicked();

                break;

            case ButtonType.Magic:
                OnMagicClick(currentButtonConstructs[2]);

                break;

            case ButtonType.Item:
                OnButtonClicked();

                break;

            case ButtonType.Move:
                OnButtonClicked();

                break;

            case ButtonType.RunAway:
                OnButtonClicked();

                break;

            case ButtonType.Swap:
                OnButtonClicked();

                break;

            case ButtonType.Enemy:
                // if (currentMenuScreen == MenuScreen.Attack)

                break;

            case ButtonType.Spell:

                break;
        }

        switch (currentMenuScreen)
        {

            case MenuScreen.NotYourTurn:
                // Debug.Log("State: Not your turn");

                break;

            case MenuScreen.Main:
                ClearIndicationArrows();
                break;

            case MenuScreen.Attack:
                UpdateIndicationArrows();
                CheckForBackButton();

                break;

            case MenuScreen.Magic:
                CheckForBackButton();
                break;

            case MenuScreen.PickingTarget:
                UpdateIndicationArrows();
                CheckForBackButton();

                break;

            case MenuScreen.Item:
                // Debug.Log("State: Item");

                break;

            case MenuScreen.Defend:
                // Debug.Log("State: Defend");

                break;

            case MenuScreen.SwapCharacter:
                // Debug.Log("State: Swap Character");

                break;

            case MenuScreen.RunAway:
                // Debug.Log("State: Run Away");

                break;
        }
    }

    public void CheckForBackButton()
    {
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            SwitchStateBack();
        }

    }

    public void SwitchState(MenuScreen newState)
    {
        currentMenuScreen = newState;
        menuScreenList.Add(currentMenuScreen);
        UpdateCurrentButtons();
    }

    public void SwitchStateBack()
    {
        currentMenuScreen = menuScreenList[menuScreenList.Count - 2];
        menuScreenList.RemoveAt(menuScreenList.Count - 1);
        UpdateCurrentButtons();
    }

    public void SwitchButtonState(ButtonType btnType)
    {
        currentButtonType = btnType;
    }

    public void UpdateCurrentButtons()
    {
        int i = 0;

        foreach (MenuButtonConstruct menuButtonConstruct in PickListType())
        {
            if (menuButtonConstruct.menuScreenName == currentMenuScreen || menuButtonConstruct.secondaryMenuScreenName == currentMenuScreen && i < buttonLocations.Length)
            {
                
                currentButtonConstructs[i] = menuButtonConstruct;
                buttonLocations[i].gameObject.SetActive(true);
                buttonLocations[i].GetComponent<TMP_Text>().text = menuButtonConstruct.menuButtonText;
                buttonLocations[i].onClick.RemoveAllListeners();
                buttonLocations[i].onClick.AddListener(delegate { SwitchButtonState(menuButtonConstruct.buttonTypeName); });
                if (currentMenuScreen == MenuScreen.Attack)
                {
                    GameObject target = _battleManager.activeEnemies[i];
                    buttonLocations[i].onClick.AddListener(delegate { SetEnemyAttackButton(target); });
                }
                if (currentMenuScreen == MenuScreen.Magic)
                {
                    if (_battleManager.currentTurnChar.knownSpellsComponents[i].doesRequireTarget) buttonLocations[i].onClick.AddListener(delegate { DelegateSpell(); });
                    else
                    {
                        int spellNum = i;
                        buttonLocations[i].onClick.AddListener(delegate { CastSpell(_battleManager.currentTurnChar, spellNum); });
                    }

                }
                if (currentMenuScreen == MenuScreen.PickingTarget)
                {
                    GameObject target = _battleManager.activeEnemies[i];
                    int spellNum = i;
                    buttonLocations[i].onClick.AddListener(delegate { CastSpell(_battleManager.currentTurnChar, spellNum, target.GetComponent<Character>()); });
                }
                
                i++;
            }
        }

        for (int j = i; j < buttonLocations.Length; j++)
        {
            buttonLocations[j].gameObject.SetActive(false);
        }

        eventSystem.SetSelectedGameObject(buttonLocations[0].gameObject);
    }

    public List<MenuButtonConstruct> PickListType()
    {
        if (currentMenuScreen == MenuScreen.Main) return listOfMenuButtons;
        if (currentMenuScreen == MenuScreen.Attack || currentMenuScreen == MenuScreen.PickingTarget) return listOfEnemyButtons;
        if (currentMenuScreen == MenuScreen.Magic) return listOfMagicButtons;
        return null;
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

        
        enemyIndicationArrows[currentlySelectedButton.transform.GetSiblingIndex()].gameObject.SetActive(true);

        for (int i = 0; i < enemyIndicationArrows.Length; i++)
        {
            if (i != currentlySelectedButton.transform.GetSiblingIndex()) enemyIndicationArrows[i].gameObject.SetActive(false);
        }
    }

    public void ClearIndicationArrows()
    {
        for (int i = 0; i < enemyIndicationArrows.Length; i++)
        {
            enemyIndicationArrows[i].gameObject.SetActive(false);
        }
    }

    public void OnClickStart(GameObject button)
    {
        startButton.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
        _battleManager = Instantiate(battleManagerPrefab).GetComponent<BattleManager>();
        MainMenuButtonCreation();
        SwitchState(MenuScreen.Main);
        // ChangeMenuScreen(gameplayMenus[0], null);


        Destroy(button);
    }

    public void OnButtonClicked()
    {
        Debug.Log("You Clicked " + currentButtonType);
        SwitchButtonState(ButtonType.NoneSelected);
    }

    public void CreateNewButtons(MenuScreen menuScreenType, ButtonType buttonType, string btnText)
    {
        MenuButtonConstruct newbtn = new MenuButtonConstruct();
        newbtn.buttonTypeName = buttonType;
        newbtn.menuScreenName = menuScreenType;
        newbtn.menuButtonText = btnText;

        if (newbtn.buttonTypeName == ButtonType.Enemy) newbtn.secondaryMenuScreenName = MenuScreen.PickingTarget;

        if (newbtn.menuScreenName == MenuScreen.Main) listOfMenuButtons.Add(newbtn);
        if (newbtn.buttonTypeName == ButtonType.Enemy) listOfEnemyButtons.Add(newbtn);
        if (newbtn.menuScreenName == MenuScreen.Magic) listOfMagicButtons.Add(newbtn);
    }

    public void OnAttackClick(MenuButtonConstruct btnCon)
    {
        SwitchState(MenuScreen.Attack);
        if (btnCon.firstClick)
        {
            for (int i = 0; i < _battleManager.activeEnemies.Length; i++)
            {
                CreateNewButtons(MenuScreen.Attack, ButtonType.Enemy, _battleManager.activeEnemies[i].GetComponent<Character>().characterName);
                btnCon.firstClick = false;
            }
        }

        UpdateCurrentButtons();
        SwitchButtonState(ButtonType.NoneSelected);
    }

    public void OnMagicClick(MenuButtonConstruct btnCon)
    {
        SwitchState(MenuScreen.Magic);
        if (btnCon.firstClick)
        {
            for (int i = 0; i < _battleManager.currentTurnChar.knownSpellCount; i++)
            {
                CreateNewButtons(MenuScreen.Magic, ButtonType.Spell, _battleManager.currentTurnChar.knownSpellsComponents[i].spellName);
                btnCon.firstClick = false;
            }
        }
        if (listOfEnemyButtons.Count == 0)
        {
            for (int i = 0; i < _battleManager.activeEnemies.Length; i++)
            {
                CreateNewButtons(MenuScreen.Attack, ButtonType.Enemy, _battleManager.activeEnemies[i].GetComponent<Character>().characterName);
                currentButtonConstructs[0].firstClick = false;
            }
        }
        UpdateCurrentButtons();
        SwitchButtonState(ButtonType.NoneSelected);
    }

    public void DelegateSpell()
    {
        SwitchState(MenuScreen.PickingTarget);
        UpdateCurrentButtons();
    }

    public void CastSpell(Character character, int spellNum)
    {

        // Debug.Log("btnNum = " + btnNum);
        character.knownSpellsComponents[spellNum].SpellSelected(character);
        SwitchStateBack();
    }

    public void CastSpell(Character character, int spellNum, Character target)
    {
        Debug.Log("CastSpell spellNum = " + spellNum);
        Debug.Log("Character: " + character.name + " Enemy Target: " + target.name);
        character.knownSpellsComponents[spellNum].SpellSelected(character, target);
        // OnClickBack();
    }
    public void SetEnemyAttackButton(GameObject target)
    {
        _battleManager.PhysicalAttack(target);
        SwitchButtonState(ButtonType.NoneSelected);
        SwitchStateBack();
    }

    public void EnableActiveMenuButtons()
    {
        if (isMenuDisabled == true)
        {
            for (int i = 0; i < buttonLocations.Length; i++)
            {
                if (buttonLocations[i] != null)
                {
                    buttonLocations[i].interactable = true;
                }
            }
            eventSystem.SetSelectedGameObject(buttonLocations[0].gameObject);
            isMenuDisabled = false;

        }

    }

    public void DisableActiveMenuButtons()
    {
        if (isMenuDisabled == false)
        {
            for (int i = 0; i < buttonLocations.Length; i++)
            {
                if (buttonLocations[i] != null) buttonLocations[i].interactable = false;
            }

            isMenuDisabled = true;
        }

    }

    [System.Serializable]
    public class MenuButtonConstruct
    {
        public MenuScreen menuScreenName;
        public MenuScreen secondaryMenuScreenName;
        public ButtonType buttonTypeName;
        
        public string menuButtonText;
        public bool firstClick = true;
        
    }
}

public enum MenuScreen
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

public enum ButtonType
{
    NoneSelected,
    Attack,
    Defend,
    Magic,
    Item,
    Move,
    Swap,
    RunAway,
    Enemy,
    Spell
}
