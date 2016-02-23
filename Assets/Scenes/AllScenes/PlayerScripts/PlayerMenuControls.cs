using UnityEngine;

public class PlayerMenuControls : MonoBehaviour {

    private bool isInGameMenuOpen = false;
    public bool IsInGameMenuOpen
    {
        get { return isInGameMenuOpen; }
        set { isInGameMenuOpen = value; }
    }

    private InGameMenuMenager menuManager;
    private InGameMenu EscMenu;
    private InGameMenu InventoryMenu;
    private GameObject CardMenu; //menu je tipa GameObject jer je fullscreen
    private GameObject WorldMapMenu;
    private GameObject AbilityBookMenu;

    void Start()
    {
        InitializeMenus();

        menuManager.OnMenuClosing += menuManager_OnMenuClosing;
        menuManager.OnMenuOpening += menuManager_OnMenuOpening;
    }

    private void InitializeMenus()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        menuManager = canvas.GetComponent<InGameMenuMenager>();
        EscMenu = canvas.Find("EscMenu").GetComponent<InGameMenu>();
        InventoryMenu = canvas.Find("InventoryMenu").GetComponent<InGameMenu>();
        CardMenu = canvas.Find("CardMenu").gameObject;
        WorldMapMenu = canvas.Find("WorldMapMenu").gameObject;
        AbilityBookMenu = canvas.Find("AbilityBookMenu").gameObject;
    }

    void menuManager_OnMenuOpening(object sender)
    {
        isInGameMenuOpen = true;
    }

    void menuManager_OnMenuClosing(object sender)
    {
        isInGameMenuOpen = false;
    }

    void Update () {
        //samo jedan menu moze biti aktivan na screenu, bilo on fulscreen ili ne
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu(EscMenu);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenMenu(InventoryMenu);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenFullScreenMenu(CardMenu);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenFullScreenMenu(AbilityBookMenu);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenFullScreenMenu(WorldMapMenu);
        }
    }

    private void OpenMenu(InGameMenu menu)
    {
        if (isInGameMenuOpen == false)
        {
            menuManager.LoadMenu(menu);
            isInGameMenuOpen = true;
        }
        else
        {
            menuManager.CloseMenu();
            isInGameMenuOpen = false;
        }
    }

    private void OpenFullScreenMenu(GameObject menu)
    {
        if (IsInGameMenuOpen == false)
        {
            menuManager.LoadFullScreenMenu(menu);
            IsInGameMenuOpen = true;
        }
        else
        {
            menuManager.CloseMenu();
            IsInGameMenuOpen = false;
        }
    }
}