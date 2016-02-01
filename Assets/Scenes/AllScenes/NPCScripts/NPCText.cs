using UnityEngine;
using System.Collections;

public class NPCText : MonoBehaviour {

    public string HelloText;
    public string GoodbyeText;

	void Start () {
        if (HelloText == "")
        {
            HelloText = "Hello " + CurrentPlayer.currentPlayer.PlayerName;
        }
        if (GoodbyeText == "")
        {
            GoodbyeText = "Good luck " + CurrentPlayer.currentPlayer.PlayerName;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
