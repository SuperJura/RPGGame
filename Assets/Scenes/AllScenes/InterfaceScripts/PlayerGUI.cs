using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerGUI : MonoBehaviour {

    public float height = 2;
    public Canvas myCanvas;

    private TextMesh myTextMesh;
    private Transform myTransform;
    private PlayerCombat playerCombat;
    private RectTransform charAbilitys; //GUI panel za Abilitys
    private Image experienceBar;
    private string playerName;

    void Start () {

        myTextMesh = GetComponent<TextMesh>();
        playerCombat = GetComponentInParent<PlayerCombat>();
        myTransform = GetComponent<Transform>();
        charAbilitys = myCanvas.transform.Find("CharAbilitys").GetComponent<RectTransform>();
        experienceBar = myCanvas.transform.Find("ExperienceBar/Experience").GetComponent<Image>();

        CurrentPlayer.currentPlayer.PlayerInventory.OnEquipmentChanged += PlayerInventory_OnEquipmentChanged;
        CurrentPlayer.currentPlayer.OnStatsChanged += currentPlayer_OnStatsChanged;
        CurrentPlayer.currentPlayer.OnLevelUp += CurrentPlayer_OnLevelUp;
        playerCombat.OnTargetChanged += playerCombat_OnTargetFocusChanged;
        playerCombat.OnAbiliyFired += PlayerCombat_OnAbiliyFired;
        playerCombat.OnAbilityCooldown += PlayerCombat_OnAbilityCooldown;
        playerCombat.OnExperienceGained += PlayerCombat_OnExperienceGained;

        FillNamePlate();
        FillAbilitysGUI();
        FillInfoPanel();
        FillExperienceBar();
	}

    private void CurrentPlayer_OnLevelUp()
    {
        Debug.Log("CurrentPlayer_OnLevelUp");
        FillInfoPanel();
        FillExperienceBar();
    }

    private void PlayerCombat_OnExperienceGained()
    {
        FillExperienceBar();
    }

    private void FillExperienceBar()
    {
        float fill = CurrentPlayer.currentPlayer.Experience / (float)CurrentPlayer.currentPlayer.ExperienceForNextLevel;
        string expText = CurrentPlayer.currentPlayer.Experience + @"/" + CurrentPlayer.currentPlayer.ExperienceForNextLevel + "(exp)";
        experienceBar.fillAmount = fill;
        experienceBar.transform.parent.Find("ExperienceText").GetComponent<Text>().text = expText;
    }

    void currentPlayer_OnStatsChanged()
    {
        FillInfoPanel();
    }

    private void FillInfoPanel()
    {
        string charClass = CurrentPlayer.currentPlayer.CharClass.ToString();
        charClass += " " + CurrentPlayer.currentPlayer.PlayerLvl;
        myCanvas.transform.Find("infoPanel/txtClass").GetComponent<Text>().text = charClass;

        string magicDmg = CurrentPlayer.currentPlayer.CalcMagicDMG().ToString();
        myCanvas.transform.Find("infoPanel/txtMagicDmg").GetComponent<Text>().text = magicDmg;

        string PhysDmg = CurrentPlayer.currentPlayer.CalcPhysDMG().ToString();
        myCanvas.transform.Find("infoPanel/txtPhysDmg").GetComponent<Text>().text = PhysDmg;

        string health = "Health " + CurrentPlayer.currentPlayer.CurrentHealth +
            " / " + CurrentPlayer.currentPlayer.MaxHealth;
        myCanvas.transform.Find("infoPanel/txtHealth").GetComponent<Text>().text = health;

        string mana = "Mana " + CurrentPlayer.currentPlayer.CurrentMana +
            " / " + CurrentPlayer.currentPlayer.MaxMana;
        myCanvas.transform.Find("infoPanel/txtMana").GetComponent<Text>().text = mana;
    }

    private void FillNamePlate()
    {
        try
        {
            playerName = "<" + CurrentPlayer.currentPlayer.PlayerName + ">";
        }
        catch (Exception ex)
        {
            playerName = "<Joe>";
            Debug.Log(ex.Message);
        }

        myTransform.position = myTransform.parent.position + new Vector3(0, height, 0);

        myTextMesh.text = playerName;
    }

    public void FillAbilitysGUI()
    {
        SetAbilityButtonGUI("Ability1", CurrentPlayer.currentPlayer.Class.Ability1);
        SetAbilityButtonGUI("Ability2", CurrentPlayer.currentPlayer.Class.Ability2);
        SetAbilityButtonGUI("Ability3", CurrentPlayer.currentPlayer.Class.Ability3);
        SetAbilityButtonGUI("Ability4", CurrentPlayer.currentPlayer.Class.Ability4);
    }

    private void SetAbilityButtonGUI(string path, Ability ab)
    {
        Button btnAb = charAbilitys.Find(path).GetComponent<Button>();
        ColorBlock cb = btnAb.colors;
        btnAb.colors = cb;

        Text ability = btnAb.GetComponentInChildren<Text>();
        ability.text = ab.Name;

        Text staticID = btnAb.transform.Find("AbilityStaticID").GetComponent<Text>();
        staticID.text = ab.StaticID.ToString();
    }

    //INVENTORY HANDLERS

    void PlayerInventory_OnEquipmentChanged(Equipment oldEquip, Equipment newEquip)
    {
        FillInfoPanel();
    }

    //ENEMY INFORMATION HANDLERS

    void playerCombat_OnTargetFocusChanged(EnemyInformation info)
    {
        myCanvas.transform.Find("infoEnemyPanel/txtEnemyName").GetComponent<Text>().text = info.Name;
        string health = info.Health + " / " + info.MaxHealth;
        myCanvas.transform.Find("infoEnemyPanel/txtEnemyHealth").GetComponent<Text>().text = health;
        myCanvas.transform.Find("infoEnemyPanel/txtEnemyAttack").GetComponent<Text>().text = info.Attack.ToString();
    }

    private void PlayerCombat_OnAbiliyFired(int abilityIndex)
    {
        Image ab = GetAbilityImage(abilityIndex);
        StartCoroutine(EmulatePressing(ab));
    }

    private void PlayerCombat_OnAbilityCooldown(float cooldown, int index)
    {
        Image ab = GetAbilityImage(index);
        ab.fillAmount = cooldown;
    }

    private Image GetAbilityImage(int abilityIndex)
    {
        Image ab = null;
        switch (abilityIndex)
        {
            case 1:
                {
                    ab = myCanvas.transform.Find("CharAbilitys/Ability1").GetComponent<Image>();
                }
                break;
            case 2:
                {
                    ab = myCanvas.transform.Find("CharAbilitys/Ability2").GetComponent<Image>();
                }
                break;
            case 3:
                {
                    ab = myCanvas.transform.Find("CharAbilitys/Ability3").GetComponent<Image>();
                }
                break;
            case 4:
                {
                    ab = myCanvas.transform.Find("CharAbilitys/Ability4").GetComponent<Image>();
                }
                break;
            default:
                break;
        }

        return ab;
    }

    private IEnumerator EmulatePressing(Image ability)
    {
        ability.color = new Color(192 / 255f, 195 / 255f, 1 / 255f);
        yield return new WaitForSeconds(0.5f);

        ability.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }
}

