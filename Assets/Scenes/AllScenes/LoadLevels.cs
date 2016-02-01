using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class LoadLevels {

    public static void LoadLevel(int lvl)
    {
        Application.LoadLevel("LEVEL_" + lvl);
    }

    public static void LoadMainMenu()
    {
        Application.LoadLevel("MainMenuScene");
    }

    public static void StartGame(Enumerations.CharClass charClass, string playerName,Enumerations.Gender M_F)
    {
       Player newPlayer = CreatePlayer.CreateNewPlayer(charClass, playerName, M_F);
       CurrentPlayer.currentPlayer = newPlayer;
       Application.LoadLevel("LEVEL_1");
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
