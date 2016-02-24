using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;

public class CreatePlayer : MonoBehaviour {

    public static Player CreateNewPlayer(Enumerations.CharClass charClass, string playerName, Enumerations.Gender M_F)
    {
        if (!Directory.Exists(Application.dataPath + "\\Saves"))
        {
            PrecreateLoading(); //stvori folder za save, config dat
        }

        Player newPlayer = new Player();

        switch (charClass)
        {
            case Enumerations.CharClass.Warrior:
                return CreateWarrior(newPlayer, playerName, M_F);
            case Enumerations.CharClass.Mage:
                return CreateMage(newPlayer, playerName, M_F);
            default:
                return null;
        }
    }

    private static Player CreateMage(Player newPlayer, string playerName, Enumerations.Gender M_F)
    {
        newPlayer.PlayerName = playerName;
        newPlayer.CharClass = Enumerations.CharClass.Mage;
        newPlayer.Class = new MageClass();
        newPlayer.Gender = M_F;
        newPlayer.IDPlayer = GetLastPlayerID();

        newPlayer.PhysicalDMG = 5;
        newPlayer.MagicDMG = 15;
        newPlayer.MoveSpeed = 20;
        newPlayer.JumpForce = 5;

        newPlayer.PhysDMGMultiplication = 1;
        newPlayer.MagicDMGMultiplication = 1;

        newPlayer.CurrentSceneName = "LEVEL_1";
        newPlayer.PosX = 179.5f;
        newPlayer.PosY = 5.6f;
        newPlayer.PosZ = 10.3f;

        newPlayer.MaxHealth = 100;
        newPlayer.CurrentHealth = 100;
        newPlayer.MaxMana = 110;
        newPlayer.CurrentMana = 110;

        newPlayer.PlayerLvl = 1;
        newPlayer.Experience = 0;

        newPlayer.PathToSave = Application.dataPath + "\\Saves\\" + newPlayer.PlayerName + "_" + newPlayer.IDPlayer + ".save";
        return SerializePlayerToDat(newPlayer);
    }

    private static Player CreateWarrior(Player newPlayer, string playerName, Enumerations.Gender M_F)
    {
        newPlayer.PlayerName = playerName;
        newPlayer.CharClass = Enumerations.CharClass.Warrior;
        newPlayer.Class = new WarriorClass();
        newPlayer.Gender = M_F;
        newPlayer.IDPlayer = GetLastPlayerID();

        newPlayer.PhysicalDMG = 15;
        newPlayer.MagicDMG = 5;
        newPlayer.MoveSpeed = 20;
        newPlayer.JumpForce = 5;

        newPlayer.PhysDMGMultiplication = 1;
        newPlayer.MagicDMGMultiplication = 1;

        newPlayer.CurrentSceneName = "LEVEL_1";
        newPlayer.PosX = 179.5f;
        newPlayer.PosY = 5.6f;
        newPlayer.PosZ = 10.3f;

        newPlayer.MaxHealth = 110;
        newPlayer.CurrentHealth = 110;
        newPlayer.MaxMana = 80;
        newPlayer.CurrentMana = 80;

        newPlayer.PlayerLvl = 1;
        newPlayer.Experience = 550;     //ZA DEBUG

        newPlayer.PathToSave = Application.dataPath + "\\Saves\\" + newPlayer.PlayerName + "_" + newPlayer.IDPlayer + ".save";
        return SerializePlayerToDat(newPlayer);
    }

    private static Player SerializePlayerToDat(Player newPlayer)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream saveFile = File.Create(newPlayer.PathToSave);
            bf.Serialize(saveFile, newPlayer);

            saveFile.Flush();
            saveFile.Close();
            Debug.Log("Finished creating player");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return newPlayer;
    }

    private static void PrecreateLoading()
    {
        XDocument doc = new XDocument(
            new XElement("root",
                new XElement("PLAYER_ID", 0)
                )
            );

        Directory.CreateDirectory(Application.dataPath + "\\Configuration");

        string configPath = Application.dataPath + "\\Configuration\\Configuration.xml";
        File.WriteAllText(configPath, doc.ToString());

        Directory.CreateDirectory(Application.dataPath + "\\Saves");
    }

    private static int GetLastPlayerID()
    {
        XDocument doc = XDocument.Load(Application.dataPath + "\\Configuration\\Configuration.xml");
        int id = int.Parse(doc.XPathSelectElement("root/PLAYER_ID").Value);
        doc.XPathSelectElement("root/PLAYER_ID").SetValue(id + 1);

        doc.Save(Application.dataPath + "\\Configuration\\Configuration.xml");
        return id;
    }

}