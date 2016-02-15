using UnityEngine;

public class PlayerMenuControls : MonoBehaviour {

    private bool isInGameMenuOpen = false;
    public bool IsInGameMenuOpen
    {
        get { return isInGameMenuOpen; }
        set { isInGameMenuOpen = value; }
    }

    public InGameMenuMenager menuManager;
    public InGameMenu EscMenu;
    public InGameMenu InventoryMenu;
    public GameObject CardMenu; //menu je tipa GameObject jer je fullscreen
    public GameObject WorldMapMenu;
    public GameObject AbilityBookMenu;

    void Start()
    {
        menuManager.OnMenuClosing += menuManager_OnMenuClosing;
        menuManager.OnMenuOpening += menuManager_OnMenuOpening;
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