using UnityEngine;

public class EndPointTrigger : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetPlayerPositionToSouthSpawn();
            LoadLevels.LoadOpenWorld();
        }
    }

    private static void SetPlayerPositionToSouthSpawn()
    {
        OpenWorldSpawnPoints.Coordinates south = OpenWorldSpawnPoints.southSpawn;
        CurrentPlayer.currentPlayer.PosX = south.x;
        CurrentPlayer.currentPlayer.PosY = south.y;
        CurrentPlayer.currentPlayer.PosZ = south.z;
    }
}