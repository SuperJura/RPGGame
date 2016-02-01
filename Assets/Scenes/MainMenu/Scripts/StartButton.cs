using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

    public Text txtClass;
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

        switch (txtClass.text)
        {
            case "Warrior":
                charClass = Enumerations.CharClass.Warrior;
                break;
            case "Mage":
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
