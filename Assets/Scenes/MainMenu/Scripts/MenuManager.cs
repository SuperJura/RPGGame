using UnityEngine;

public class MenuManager : MonoBehaviour {

    public Menu SelectedMenu;

	void Start () {
        LoadMenu(SelectedMenu);
	}

    public void LoadMenu(Menu newMenu)
    {
        if (SelectedMenu != null)
        {
            SelectedMenu.IsOpen = false;
        }

        SelectedMenu = newMenu;
        SelectedMenu.IsOpen = true;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
