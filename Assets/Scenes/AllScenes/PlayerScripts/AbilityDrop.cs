using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class AbilityDrop : MonoBehaviour, IDropHandler {

    private PlayerGUI playerGUI;

    void Start()
    {
        playerGUI = GameObject.Find("PlayerObject/NamePlate").GetComponent<PlayerGUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        int newStaticID = int.Parse(eventData.pointerDrag.transform.Find("AbilityStaticID").GetComponent<Text>().text);
        int oldStaticID = int.Parse(transform.Find("AbilityStaticID").GetComponent<Text>().text);

        SwitchAbilitys(oldStaticID, newStaticID);
        playerGUI.FillAbilitysGUI();
    }

    private void SwitchAbilitys(int oldStaticID, int newStaticID)
    {

        if (oldStaticID == newStaticID || !IfAlreadyHasThatAbility(newStaticID))
        {
            Debug.Log("neuspjesno promjenjen ability");
            return;
        }

        if (CurrentPlayer.currentPlayer.Class.ChangeAbility(oldStaticID, newStaticID))
        {
            Debug.Log("Uspjesno promjenjen ability");
        }
        else
        {
            Debug.Log("neuspjesno promjenjen ability");
        }
        
    }

    private bool IfAlreadyHasThatAbility(int newStaticID)
    {
        if (CurrentPlayer.currentPlayer.Class.Ability1.StaticID == newStaticID)
        {
            return false;
        }
        if (CurrentPlayer.currentPlayer.Class.Ability2.StaticID == newStaticID)
        {
            return false;
        }
        if (CurrentPlayer.currentPlayer.Class.Ability3.StaticID == newStaticID)
        {
            return false;
        }
        if (CurrentPlayer.currentPlayer.Class.Ability4.StaticID == newStaticID)
        {
            return false;
        }
        return true;
    }
}