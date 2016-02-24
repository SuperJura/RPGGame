using UnityEngine;

public class SceneLoaded : MonoBehaviour
{

    void Awake()
    {
        //Debug.Log(CurrentPlayer.currentPlayer.PlayerInventory.PlayerInventory.Count);
        InitializeCanvas();
    }

    void Start()
    {
        CurrentPlayer.currentPlayer.CurrentSceneName = "OpenWorldScene";
    }

    private static void InitializeCanvas()
    {
        GameObject go = (GameObject)Resources.Load("Canvas");
        RectTransform canvas = Instantiate((RectTransform)go.transform);

        canvas.name = "Canvas";
        canvas.SetParent(null);
    }
}