using UnityEngine;

public class SceneLoaded : MonoBehaviour
{

    void Awake()
    {
        //Debug.Log(CurrentPlayer.currentPlayer.PlayerInventory.PlayerInventory.Count);
        GameObject go = (GameObject)Resources.Load("Canvas");
        RectTransform canvas = Instantiate((RectTransform)go.transform);

        canvas.name = "Canvas";
        canvas.SetParent(null);
    }
}