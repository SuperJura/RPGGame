using UnityEngine;

public class InGameMenuMenager : MonoBehaviour
{
    public PlayerControls playerControls;
    public InGameMenu SelectedMenu;
    public GameObject SelectedFullscreenMenu;

    public delegate void OnMenuClosingHandler(object sender);
    public event OnMenuClosingHandler OnMenuClosing;

    public delegate void OnMenuOpeningHandler(object sender);
    public event OnMenuOpeningHandler OnMenuOpening;


    public void LoadMenu(InGameMenu newMenu)
    {
        if (SelectedMenu != null)
        {
            CloseSelectedMenu();
        }

        SelectedMenu = newMenu;
        SelectedMenu.IsOpen = true;

        playerControls.enabled = false;

        OnMenuOpening(this);
    }

    public void CloseMenu()
    {
        CloseSelectedMenu();
        playerControls.enabled = true;
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

        playerControls.enabled = false;

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