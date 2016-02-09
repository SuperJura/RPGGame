using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    
    public Dropdown ddlClass;
    public Text txtName;
    public Toggle cBoxMale; //ako nije male onda je female


    public void btnStartClick()
    {
        Enumerations.CharClass charClass;
        Enumerations.Gender gender;

        if (txtName.text == "")  //ako nije unio ime, nemoze poceti
        {
            return;
        }

        switch (ddlClass.value)
        {
            case 0:
                charClass = Enumerations.CharClass.Warrior;
                break;
            case 1:
                charClass = Enumerations.CharClass.Mage;
                break;
            default:
                return; //ako nije izabrao lika, nemoze poceti game
        }

        switch (cBoxMale.isOn)
        {
            case true:
                gender = Enumerations.Gender.Male;
                break;
            case false:
                gender = Enumerations.Gender.Female;
                break;
            default:
                gender = Enumerations.Gender.Male;
                break;
        }

        LoadLevels.StartGame(charClass, txtName.text, gender);
    }
}
