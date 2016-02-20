using UnityEngine;

public class NPCText : MonoBehaviour {

    public string HelloText;
    public string GoodbyeText;
    public float CharacterSize;

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
}