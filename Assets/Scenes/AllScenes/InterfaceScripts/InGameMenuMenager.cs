﻿using UnityEngine;
using System.Collections;

public class InGameMenuMenager : MonoBehaviour
{
    public InGameMenu SelectedMenu;
    public GameObject SelectedFullscreenMenu;

    public delegate void OnMenuClosingHandler(object sender);
    public event OnMenuClosingHandler OnMenuClosing;

    public delegate void OnMenuOpeningHandler(object sender);
    public event OnMenuOpeningHandler OnMenuOpening;

    private PlayerControls playerControls;
    private CameraControl cameraControl;

    void Start()
    {
        playerControls = GameObject.Find("PlayerObject").GetComponent<PlayerControls>();
        cameraControl = Camera.main.transform.GetComponent<CameraControl>();
    }

    public void LoadMenu(InGameMenu newMenu)
    {
        if (SelectedMenu != null)
        {
            CloseSelectedMenu();
        }

        SelectedMenu = newMenu;
        SelectedMenu.IsOpen = true;

        ChangeEnableOnControls();

        OnMenuOpening(this);
    }

    private void ChangeEnableOnControls()
    {
        playerControls.enabled = !playerControls.enabled;
        cameraControl.enabled = !cameraControl.enabled;
    }

    public void CloseMenu()
    {
        CloseSelectedMenu();
        ChangeEnableOnControls();
        OnMenuClosing(this);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void LoadFullScreenMenu(GameObject newMenu)
    {
        CloseSelectedMenu();
        SelectedFullscreenMenu = newMenu;
        SelectedFullscreenMenu.GetComponent<CanvasGroup>().alpha = 1;
        SelectedFullscreenMenu.GetComponent<CanvasGroup>().interactable = true;
        SelectedFullscreenMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;

        ChangeEnableOnControls();

        OnMenuOpening(this);
    }

    private void CloseSelectedMenu()
    {
        if (SelectedMenu != null)
        {
            if (SelectedMenu is InGameMenu)
            {
                SelectedMenu.IsOpen = false;
            }
        }
        if (SelectedFullscreenMenu != null)
        {
            SelectedFullscreenMenu.GetComponent<CanvasGroup>().alpha = 0;
            SelectedFullscreenMenu.GetComponent<CanvasGroup>().interactable = false;
            SelectedFullscreenMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}