using UnityEngine;

public class LoadCurrentPlayerInGame : MonoBehaviour {

    private Player player;

    void Awake()
    {
        if (CurrentPlayer.currentPlayer == null)
        {
            CurrentPlayer.currentPlayer = new Player();
            CurrentPlayer.currentPlayer.PlayerInventory.OnEquipmentChanged += CurrentPlayer.currentPlayer.CalcNewEquiped;
            CurrentPlayer.currentPlayer.CharClass = Enumerations.CharClass.Warrior;
            CurrentPlayer.currentPlayer.Class = new WarriorClass();
            CurrentPlayer.currentPlayer.Gender = Enumerations.Gender.Male;
            CurrentPlayer.currentPlayer.IDLevel = 1;
            CurrentPlayer.currentPlayer.IDPlayer = -1;
            CurrentPlayer.currentPlayer.JumpForce = 6;
            CurrentPlayer.currentPlayer.MagicDMG = 50;
            CurrentPlayer.currentPlayer.MaxMana = 100;
            CurrentPlayer.currentPlayer.CurrentMana = 100;
            CurrentPlayer.currentPlayer.MaxHealth = 100;
            CurrentPlayer.currentPlayer.CurrentHealth = 100;
            CurrentPlayer.currentPlayer.MoveSpeed = 50;
            CurrentPlayer.currentPlayer.PhysicalDMG = 10;
            CurrentPlayer.currentPlayer.PlayerName = "me";
            CurrentPlayer.currentPlayer.PosX = 0;
            CurrentPlayer.currentPlayer.PosY = 15;
            CurrentPlayer.currentPlayer.PosZ = 0;
            CurrentPlayer.currentPlayer.PlayerLvl = 1;

            CurrentPlayer.currentPlayer.PhysDMGMultiplication = 1;
            CurrentPlayer.currentPlayer.MagicDMGMultiplication = 1;
        }
        else
        {
            CurrentPlayer.currentPlayer.PlayerInventory.OnEquipmentChanged += CurrentPlayer.currentPlayer.CalcNewEquiped;
        }
        player = CurrentPlayer.currentPlayer;

        Vector3 newPosition = new Vector3(player.PosX, player.PosY, player.PosZ);
        transform.position = newPosition;

        switch (player.CharClass)
        {
            case Enumerations.CharClass.Warrior:
                GetComponent<Renderer>().material = (Material)Resources.Load("MatWarrior", typeof(Material));
                break;
            case Enumerations.CharClass.Mage:
                GetComponent<Renderer>().material = (Material)Resources.Load("MatMage", typeof(Material));
                break;
            default:
                break;
        }
        
	}
}