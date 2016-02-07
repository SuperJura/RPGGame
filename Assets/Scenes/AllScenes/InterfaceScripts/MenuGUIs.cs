using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MenuGUIs : MonoBehaviour {

    public Transform CardItemList;
    public Transform InventoryItemList;  //inventory panel
    public Transform AbilityBookMenu;

    private char selectedOrder; // n=byName, q=byQuality, s=bySlot
    
    void Start ()
    {
        CurrentPlayer.currentPlayer.PlayerInventory.OnInventoryChanged += PlayerInventory_OnInventoryChanged;
        CurrentPlayer.currentPlayer.OnLevelUp += CurrentPlayer_OnLevelUp;

        LoadCardsInCardMenu();
        LoadItemsInInventory();
        LoadAbilitysInAbilityBook();
    }

    //CARD METHODS
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

    //INVENTORY METHODS
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

            Toggle tog = panel.Find("Panel/Equip").GetComponent<Toggle>();

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
                    if (child.Find("Panel/Equip").GetComponent<Toggle>().isOn == true && sender.isOn == true)
                    {
                        child.Find("Panel/Equip").GetComponent<Toggle>().isOn = false;    //promjeni ga u false, tj skini ga
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
            delegate (Equipment e1, Equipment e2)
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
            delegate (Equipment e1, Equipment e2)
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
            delegate (Equipment e1, Equipment e2)
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

    //ABILITY METHODS

    private void LoadAbilitysInAbilityBook()
    {
        ClearAllChildren();
        const int MAX_LEVEL = 2;
        IAbilityDatabase ablityDatabase = Repository.GetAbilityDatabaseInstance();
        Enumerations.CharClass CharClass = CurrentPlayer.currentPlayer.CharClass;
        int currentPlayerLevel = CurrentPlayer.currentPlayer.PlayerLvl;
        int numberOfPages = 1;
        int numberOfAbilitys = 0;

        RectTransform currentPage = CreateNewPage(1);
        for (int level = 1; level <= MAX_LEVEL; level++)
        {
            foreach (Ability ab in ablityDatabase.GetAbilitys(CharClass, level))
            {
                if (numberOfAbilitys >= 6)
                {
                    numberOfAbilitys = 0;
                    numberOfPages++;
                    currentPage = CreateNewPage(numberOfPages);
                }
                CreateNewAbilitySlot(currentPage, ab, level, currentPlayerLevel);
                numberOfAbilitys++;
            }
        }
    }

    private void ClearAllChildren()
    {
        foreach (Transform child in AbilityBookMenu)
        {
            Destroy(child.gameObject);
        }
        Transform buttons = AbilityBookMenu.parent.Find("AbilityBookPageButtons");
        foreach (Transform child in buttons)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateNewAbilitySlot(RectTransform currentPage, Ability ab,int abilityLevel, int currentPlayerLevel)
    {
        GameObject go = (GameObject)Resources.Load("AbilitySlot");
        RectTransform prefab = (RectTransform)go.transform;
        RectTransform abilitySlot = Instantiate(prefab);

        abilitySlot.GetComponentInChildren<Text>().text = ab.Name;
        abilitySlot.transform.Find("AbilityLevel").GetComponent<Text>().text = abilityLevel.ToString();
        abilitySlot.Find("AbilityStaticID").GetComponent<Text>().text = ab.StaticID.ToString();
        abilitySlot.gameObject.AddComponent<AbilityMouseOverTooltip>();

        if (abilityLevel > currentPlayerLevel)
        {
            abilitySlot.GetComponent<Button>().interactable = false;
            abilitySlot.GetComponent<AbilityDrag>().enabled = false;
        }
        abilitySlot.SetParent(currentPage);
    }

    private RectTransform CreateNewPage(int pageNumber)
    {
        GameObject go = (GameObject)Resources.Load("AbilityBookPage");
        RectTransform prefab = (RectTransform)go.transform;
        RectTransform page = Instantiate(prefab);
        page.SetParent(AbilityBookMenu, false);
        page.name = "Page" + pageNumber;
        page.GetComponentInChildren<Text>().text = pageNumber.ToString();

        AddPageButton(pageNumber);
        return page;
    }

    private void AddPageButton(int pageNumber)
    {
        GameObject go = (GameObject)Resources.Load("AbilityBookPageButton");
        RectTransform prefab = (RectTransform)go.transform;
        RectTransform button = Instantiate(prefab);

        button.GetComponentInChildren<Text>().text = pageNumber.ToString();
        button.SetParent(AbilityBookMenu.parent.Find("AbilityBookPageButtons"));

        button.GetComponent<Button>().onClick.AddListener(() => PageButton_Click(button));
    }

    private void PageButton_Click(object sender)
    {
        RectTransform b = (RectTransform)sender;
        string pageNumber = b.GetComponent<Button>().GetComponentInChildren<Text>().text;

        RectTransform targetPage = AbilityBookMenu.Find("Page" + pageNumber).GetComponent<RectTransform>();
        targetPage.SetAsLastSibling();

    }

    private void CurrentPlayer_OnLevelUp()
    {
        LoadAbilitysInAbilityBook();
    }
}