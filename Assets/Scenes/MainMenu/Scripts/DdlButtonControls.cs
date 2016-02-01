using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DdlButtonControls : MonoBehaviour {

    List<Button> Items;

	// Use this for initialization
	void Start () {

        Items = new List<Button>();
        Items.AddRange(GetComponentsInChildren<Button>());

        foreach (Button b in Items)
        {
            if (b.name == this.name)
            {
                Items.Remove(b);
                break;
            }
        }

        ChangeAlphaOfChildren();
	}

    public void ChangeAlphaOfChildren()
    {
        foreach (Button b in Items)
        {
            ChangeAlpha(b.GetComponent<CanvasGroup>());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    private void ChangeAlpha(CanvasGroup canvasGrp)
    {
        if (canvasGrp.alpha == 1)
        {
            canvasGrp.alpha = 0;
        }
        else
        {
            canvasGrp.alpha = 1;
        }

        canvasGrp.blocksRaycasts = !canvasGrp.blocksRaycasts;
        canvasGrp.interactable = !canvasGrp.interactable;
    }

    public void ChangeClass(string charClass)
    {
        GetComponentInChildren<Text>().text = charClass;
    }
}
