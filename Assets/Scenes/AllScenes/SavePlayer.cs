using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SavePlayer : MonoBehaviour {

    private Transform player;

    public void SavePlayerAndExit()
    {
        SavePlayerInDat();

        Application.Quit();
    }

    public void SavePlayerAndLoadMainMenu()
    {
        SavePlayerInDat();

        SceneManager.LoadScene("MainMenuScene");
    }

    private void SavePlayerInDat()
    {
        player = GameObject.Find("PlayerObject").transform;

        CurrentPlayer.currentPlayer.PosX = player.position.x;
        CurrentPlayer.currentPlayer.PosY = player.position.y;
        CurrentPlayer.currentPlayer.PosZ = player.position.z;

        FileStream fs = new FileStream(CurrentPlayer.currentPlayer.PathToSave, FileMode.Create, FileAccess.ReadWrite);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, CurrentPlayer.currentPlayer);
    }
}