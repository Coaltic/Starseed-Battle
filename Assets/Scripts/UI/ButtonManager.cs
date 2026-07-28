using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public Button[] buttonLocations;
    public List<MenuButtonConstruct> listOfButtons;
    public EventSystem eventSystem;
    public GameObject buttonContainer;

    public MenuScreen currentMenuScreen;
    public ButtonType currentButtonType;
    public bool onceATurn;


    public MenuScreen previousMenuScreen;
    public GameObject startButton;

    public GameObject[] gameplayMenus;
    public Button[] activeMenuButtons;
    public GameObject gameplayMenuHUD;
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
        SwitchState(MenuScreen.Start);
        
    }

    void Start()
    {
        // LoadButtonText();
        eventSystem.SetSelectedGameObject(startButton);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentButtonType)
        {

            case ButtonType.Attack:
                OnButtonClicked();

                break;

            case ButtonType.Defend:
                OnButtonClicked();

                break;

            case ButtonType.Item:
                OnButtonClicked();

                break;

            case ButtonType.Magic:
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

                break;

            case MenuScreen.Magic:

                break;

            case MenuScreen.PickingTarget:
                UpdateIndicationArrows();

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

    public void SwitchState(MenuScreen newState)
    {
        currentMenuScreen = newState;
        menuScreenList.Add(currentMenuScreen);
        LoadButtonText();
    }

    public void SwitchStateBack()
    {
        currentMenuScreen = menuScreenList[menuScreenList.Count - 1];
    }

    public void SwitchButtonState(ButtonType btnType)
    {
        currentButtonType = btnType;
    }

    public void LoadButtonText()
    {
        int i = 0;
        foreach (MenuButtonConstruct menuButtonConstruct in listOfButtons)
        {
            if (menuButtonConstruct.menuScreenName == currentMenuScreen && i < buttonLocations.Length)
            {
                buttonLocations[i].gameObject.SetActive(true);
                buttonLocations[i].GetComponent<TMP_Text>().text = menuButtonConstruct.menuButtonText;
                buttonLocations[i].onClick.AddListener(delegate { SwitchButtonState(menuButtonConstruct.buttonTypeName); });
                i++;
            }
        }

        for (int j = i; j < buttonLocations.Length; j++)
        {
            buttonLocations[j].gameObject.SetActive(false);
        }
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

    public void OnClickStart(GameObject button)
    {
        startButton.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
        _battleManager = Instantiate(battleManagerPrefab).GetComponent<BattleManager>();
        SwitchState(MenuScreen.Main);
        // ChangeMenuScreen(gameplayMenus[0], null);


        Destroy(button);
    }

    public void OnButtonClicked()
    {
        Debug.Log("You Clicked " + currentButtonType);
        SwitchButtonState(ButtonType.NoneSelected);
    }

    public void OnClickBack()
    {
        // ChangeMenuScreenBack(previousMenuScreensList[previousMenuScreensList.Count - 2], previousMenuScreensList[previousMenuScreensList.Count - 1]);
    }

    public void CreateButtonLocations()
    {

    }

    public void OnAttackClick()
    {
        for (int i = 0; i < buttonLocations.Length; i++)
        {
            if (i <= _battleManager.activeEnemies.Length)
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
                buttonLocations[i].gameObject.SetActive(false);
            }
        }
        // ChangeMenuScreen(gameplayMenus[1], gameplayMenus[0]);
    }

    public void SetEnemyAttackButton(GameObject target)
    {
        _battleManager.PhysicalAttack(target);
        OnClickBack();
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
    RunAway
}
