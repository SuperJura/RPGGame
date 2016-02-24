using UnityEngine;

public class CurrentPlayer : MonoBehaviour {

    public static Player currentPlayer { get; set; }

    void Start ()
    {
        DontDestroyOnLoad(this);
    }
}