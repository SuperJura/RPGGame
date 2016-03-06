using System;
using UnityEngine;

public class PlayerMenuControls : MonoBehaviour {

    private bool isInGameMenuOpen = false;
    public bool IsInGameMenuOpen
    {
        get { return isInGameMenuOpen; }
        set { isInGameMenuOpen = value; }
    }

    private InGameMenuMenager menuManager;
    private InGameMenu escMenu;
    private InGameMenu inventoryMenu;
    private GameObject cardMenu; //menu je tipa GameObject jer je fullscreen
    private GameObject worldMapMenu;
    private GameObject abilityBookMenu;

    private GameObject minimapFrame;
    private Camera minimapCamera;

    void Start()
    {
        InitializeMenus();

        minimapCamera = transform.Find("MinimapCamera").GetComponent<Camera>();
        menuManager.OnMenuClosing += menuManager_OnMenuClosing;
        menuManager.OnMenuOpening += menuManager_OnMenuOpening;
    }

    private void InitializeMenus()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        menuManager = canvas.GetComponent<InGameMenuMenager>();
        escMenu = canvas.Find("EscMenu").GetComponent<InGameMenu>();
        inventoryMenu = canvas.Find("InventoryMenu").GetComponent<InGameMenu>();
        cardMenu = canvas.Find("CardMenu").gameObject;
        worldMapMenu = canvas.Find("WorldMapMenu").gameObject;
        abilityBookMenu = canvas.Find("AbilityBookMenu").gameObject;
        minimapFrame = canvas.Find("MinimapFrame").gameObject;
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
            OpenMenu(escMenu);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenMenu(inventoryMenu);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenFullScreenMenu(cardMenu);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenFullScreenMenu(abilityBookMenu);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenFullScreenMenu(worldMapMenu);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenMinimap();
        }
    }

    private void OpenMinimap()
    {
        menuManager.ToggleMinimap(minimapFrame);
        minimapCamera.enabled = !minimapCamera.enabled;
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