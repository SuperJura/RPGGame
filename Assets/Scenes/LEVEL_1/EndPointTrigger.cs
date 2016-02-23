using UnityEngine;

public class EndPointTrigger : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LoadLevels.LoadOpenWorld();
        }
    }
}