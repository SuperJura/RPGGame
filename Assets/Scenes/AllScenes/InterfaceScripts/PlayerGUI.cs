using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerGUI : MonoBehaviour {

    private TextMesh myTextMesh;
    private Transform myTransform;
    private PlayerCombat playerCombat;
    private Transform charAbilitys; //GUI panel for Abilitys
    private string playerName;
    private char selectedOrder; // n=byName, q=byQuality, s=bySlot

    public Transform InventoryItemList;  //inventory panel
    public Transform CardItemList;
    public float height = 2;
    public Canvas myCanvas;

	// Use this for initialization
	void Start () {
        CurrentPlayer.currentPlayer.PlayerInventory.OnEquipmentChanged += PlayerInventory_OnEquipmentChanged;
        CurrentPlayer.currentPlayer.PlayerInventory.OnInventoryChanged += PlayerInventory_OnInventoryChanged;
        CurrentPlayer.currentPlayer.OnStatsChanged += currentPlayer_OnStatsChanged;
        playerCombat = GetComponentInParent<PlayerCombat>();
        playerCombat.OnTargetChanged += playerCombat_OnTargetFocusChanged;
        playerCombat.OnAbiliyFired += PlayerCombat_OnAbiliyFired;
        playerCombat.OnAbilityCooldown += PlayerCombat_OnAbilityCooldown;

        DisplayNamePlate();

        FillInfoPanel();

        LoadItemsInInventory();

        LoadCardsInCardMenu();
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
        catch (System.Exception ex)
        {
            playerName = "<Joe>";
            Debug.Log(ex.Message);
        }

        myTransform.position = myTransform.parent.position + new Vector3(0, height, 0);

        myTextMesh.text = playerName;
    }

    //INVENTORY HANDLERS

    private void LoadItemsInInventory()
    {
        foreach (RectTransform child in InventoryItemList)
        {
            Destroy(child.gameObject);
        }

        GameObject go = (GameObject)Resources.Load("itemSlot");
        RectTransform prefab = (RectTransform)go.transform;
        
        foreach (Equipment e in CurrentPlayer.currentPlayer.PlayerInventory.PlayerInventory)
        {
            RectTransform panel = GameObject.Instantiate(prefab);
            panel.Find("Panel/ItemName").GetComponentInChildren<Text>().text = e.Name;
            panel.Find("Panel/ItemSlot").GetComponentInChildren<Text>().text = e.Slot.ToString();
            panel.Find("Panel/ItemStaticID").GetComponentInChildren<Text>().text = e.StaticIDEquipment;
            panel.Find("Panel/ItemID").GetComponentInChildren<Text>().text = e.IDEquipment.ToString();
            panel.Find("Panel/ItemQuality").GetComponentInChildren<Text>().text = e.Quality.ToString();

            switch (e.Quality)
            {
                case Enumerations.EquipmentQuality.Common:
                    panel.Find("Panel/ItemQuality").GetComponentInChildren<Text>().color = Color.white;
                    break;
                case Enumerations.EquipmentQuality.Rare:
                    panel.Find("Panel/ItemQuality").GetComponentInChildren<Text>().color = Color.blue;
                    break;
                case Enumerations.EquipmentQuality.Unheard:
                    panel.Find("Panel/ItemQuality").GetComponentInChildren<Text>().color = Color.green;
                    break;
                case Enumerations.EquipmentQuality.Legendary:
                    panel.Find("Panel/ItemQuality").GetComponentInChildren<Text>().color = Color.yellow;
                    break;
            }

            Toggle tog = (Toggle)panel.Find("Panel/Equip").GetComponent<Toggle>();

            tog.onValueChanged.AddListener((on) => UnequipEquip(panel, tog));

            if (CurrentPlayer.currentPlayer.PlayerInventory.HasEquiped(e.StaticIDEquipment))
            {
                tog.isOn = true;
            }

            panel.SetParent(InventoryItemList);
        }
    }

    private void UnequipEquip(RectTransform panel, Toggle sender)
    {
        int itemID = int.Parse(panel.Find("Panel/ItemID").GetComponentInChildren<Text>().text);

        Equipment newEquip = null;  //equipment koji se zeli obuci
        foreach (Equipment e in CurrentPlayer.currentPlayer.PlayerInventory.PlayerInventory)
        {
            if (e.IDEquipment == itemID)
            {
                newEquip = e;
            }
        }

        string slot = "";
        int id;

        foreach (Transform child in InventoryItemList)   //za svaki equipment u izborniku
        {
            slot = child.Find("Panel/ItemSlot").GetComponentInChildren<Text>().text;    //nadi gdje se nalazi
            id = int.Parse(child.Find("Panel/ItemID").GetComponentInChildren<Text>().text);    //id equipmenta u izborniku

            if (slot == newEquip.Slot.ToString())   //ako se slotovi podudaraju
            {
                if (id != newEquip.IDEquipment)  //i to nije ista instanca objekta
                {
                    if (((Toggle)child.Find("Panel/Equip").GetComponent<Toggle>()).isOn == true && sender.isOn == true) 
                    {
                        ((Toggle)child.Find("Panel/Equip").GetComponent<Toggle>()).isOn = false;    //promjeni ga u false, tj skini ga
                    }
                }
            }
        }

        CurrentPlayer.currentPlayer.PlayerInventory.ChangeEquip(sender.isOn, newEquip);
    }

    public void SortItemsByName()
    {
        if (selectedOrder == 'n')
        {
            return;
        }
        CurrentPlayer.currentPlayer.PlayerInventory.PlayerInventory.Sort(
            delegate(Equipment e1, Equipment e2)
            {
                return e1.Name.CompareTo(e2.Name);
            }
            );

        LoadItemsInInventory();
        selectedOrder = 'n';
    }

    public void SortItemsByQuality()
    {
        if (selectedOrder == 'q')
        {
            return;
        }
        CurrentPlayer.currentPlayer.PlayerInventory.PlayerInventory.Sort(
            delegate(Equipment e1, Equipment e2)
            {
                return e1.Quality.CompareTo(e2.Quality);
            }
            );

        LoadItemsInInventory();

        selectedOrder = 'q';
    }

    public void SortItemsBySlot()
    {
        if (selectedOrder == 's')
        {
            
        }
        CurrentPlayer.currentPlayer.PlayerInventory.PlayerInventory.Sort(
            delegate(Equipment e1, Equipment e2)
            {
                return e1.Slot.CompareTo(e2.Slot);
            }
            );

        LoadItemsInInventory();

        selectedOrder = 's';
    }

    void PlayerInventory_OnInventoryChanged()
    {
        LoadItemsInInventory();
    }

    void PlayerInventory_OnEquipmentChanged(Equipment oldEquip, Equipment newEquip)
    {
        FillInfoPanel();
    }

    //CARD MENU HANDLERS

    private void LoadCardsInCardMenu()
    {
        foreach (RectTransform child in CardItemList)
        {
            Destroy(child.gameObject);
        }
        GameObject go = (GameObject)Resources.Load("Card");
        RectTransform prefab = (RectTransform)go.transform;
        foreach (Card c in CurrentPlayer.currentPlayer.PlayerInventory.CardCollection)
        {
            RectTransform card = GameObject.Instantiate(prefab);
            card.Find("CardName").GetComponentInChildren<Text>().text = c.Name;
            card.Find("CardInfo/CardCooldown").GetComponentInChildren<Text>().text = c.DefaultCooldown.ToString();
            card.Find("CardInfo/CardHealth").GetComponentInChildren<Text>().text = c.Health.ToString();
            card.Find("CardInfo/CardAttack").GetComponentInChildren<Text>().text = c.Attack.ToString();

            //kasnije dodaj sliku
            switch (c.Quality)
            {
                case Enumerations.EquipmentQuality.Common:
                    card.GetComponent<Image>().color = Color.white;
                    break;
                case Enumerations.EquipmentQuality.Rare:
                    card.GetComponent<Image>().color = new Color(78 / 255f, 78 / 255f, 204 / 255f);  //blue
                    break;
                case Enumerations.EquipmentQuality.Unheard:
                    card.GetComponent<Image>().color = new Color(255 / 255f, 96 / 255f, 96 / 255f);  //red
                    break;
                case Enumerations.EquipmentQuality.Legendary:
                    card.GetComponent<Image>().color = new Color(212 / 255f, 199 / 255f, 48 / 255f); //yellow
                    break;
            }

            card.SetParent(CardItemList);
        }
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

