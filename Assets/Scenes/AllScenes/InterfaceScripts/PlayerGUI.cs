using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerGUI : MonoBehaviour {

    public float height = 2;
    public Canvas myCanvas;

    private TextMesh myTextMesh;
    private Transform myTransform;
    private PlayerCombat playerCombat;
    private Transform charAbilitys; //GUI panel for Abilitys
    private string playerName;

    void Start () {
        CurrentPlayer.currentPlayer.PlayerInventory.OnEquipmentChanged += PlayerInventory_OnEquipmentChanged;
        CurrentPlayer.currentPlayer.OnStatsChanged += currentPlayer_OnStatsChanged;
        playerCombat = GetComponentInParent<PlayerCombat>();
        playerCombat.OnTargetChanged += playerCombat_OnTargetFocusChanged;
        playerCombat.OnAbiliyFired += PlayerCombat_OnAbiliyFired;
        playerCombat.OnAbilityCooldown += PlayerCombat_OnAbilityCooldown;

        DisplayNamePlate();

        FillInfoPanel();
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

        string health = "\tHealth\n" + CurrentPlayer.currentPlayer.CurrentHealth +
            " / " + CurrentPlayer.currentPlayer.MaxHealth;
        myCanvas.transform.Find("infoPanel/txtHealth").GetComponent<Text>().text = health;

        string mana = "\tMana\n" + CurrentPlayer.currentPlayer.CurrentMana +
            " / " + CurrentPlayer.currentPlayer.MaxMana;
        myCanvas.transform.Find("infoPanel/txtMana").GetComponent<Text>().text = mana;
    }

    private void DisplayNamePlate()
    {
        myTextMesh = GetComponent<TextMesh>();
        myTransform = GetComponent<Transform>();
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

