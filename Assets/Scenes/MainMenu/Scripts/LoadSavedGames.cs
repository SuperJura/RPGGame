using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadSavedGames : MonoBehaviour {

    void Start () {
        string[] allSaves = Directory.GetFiles(Application.dataPath + "\\Saves", "*.save");

        Transform myTransform = GetComponent<Transform>();
        Button prefab = Resources.Load<Button>("ResumeGamePlayer");
        foreach (string s in allSaves)
        {
            string[] allName = s.Split('\\');
            Button b = (Button)Object.Instantiate(prefab);
            b.GetComponentInChildren<Text>().text = allName[allName.Length - 1];

            b.onClick.AddListener(() => OnSavedClickListener(b));

            b.transform.SetParent(myTransform, false);
        }
	}

    private void OnSavedClickListener(Button b)
    {
        string path = Application.dataPath + "\\Saves\\" + b.GetComponentInChildren<Text>().text;

        LoadLevels.LoadSavedGame(path);
    }
}
