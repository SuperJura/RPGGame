using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuGUIs : MonoBehaviour {

    public Transform CardItemList;
    public Transform InventoryItemList;  //inventory panel

    private char selectedOrder; // n=byName, q=byQuality, s=bySlot

    // Use this for initialization
    void Start ()
    {
        CurrentPlayer.currentPlayer.PlayerInventory.OnInventoryChanged += PlayerInventory_OnInventoryChanged;

        LoadCardsInCardMenu();
        LoadItemsInInventory();
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
}
