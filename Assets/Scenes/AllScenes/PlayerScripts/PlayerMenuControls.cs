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
    public GameObject CardMenu;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInGameMenuOpen == false)
            {
                menuManager.LoadMenu(EscMenu);
                isInGameMenuOpen = true;
            }
            else
            {
                menuManager.CloseMenu();
                isInGameMenuOpen = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInGameMenuOpen == false)
            {
                menuManager.LoadMenu(InventoryMenu);
                isInGameMenuOpen = true;
            }
            else
            {
                menuManager.CloseMenu();
                isInGameMenuOpen = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isInGameMenuOpen == false)
            {
                menuManager.LoadFullScreenMenu(CardMenu);
                isInGameMenuOpen = true;
            }
            else
            {
                menuManager.CloseMenu();
                isInGameMenuOpen = false;
            }
        }
	}
}
