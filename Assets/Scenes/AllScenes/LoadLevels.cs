using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.XPath;
using UnityEngine.SceneManagement;

public static class LoadLevels {


    public static void LoadLevel(int lvl)
    {
        SceneManager.LoadScene("LEVEL_" + lvl);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public static void StartGame(Enumerations.CharClass charClass, string playerName,Enumerations.Gender M_F)
    {
       Player newPlayer = CreatePlayer.CreateNewPlayer(charClass, playerName, M_F);
       CurrentPlayer.currentPlayer = newPlayer;
        SceneManager.LoadScene("LEVEL_1");
    }

    public static void LoadSavedGame(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
        Player p = (Player)bf.Deserialize(fs);

        CurrentPlayer.currentPlayer = p;
        fs.Flush();
        fs.Close();

        LoadLevel(p.IDLevel);
    }
}
