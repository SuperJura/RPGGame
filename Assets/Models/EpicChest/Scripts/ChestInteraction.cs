using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChestInteraction : MonoBehaviour {

    public InGameMenuMenager manager;
    public InGameMenu lootMenu;
    public Transform itemList;

    private IItemDataBase itemDatabase;
    private Animation chestAnimation;
    private bool isopen;

    void Start () {
        itemDatabase = Repository.GetItemDatabaseInstance();
        chestAnimation = GetComponentInParent<Animation>();
        isopen = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.name == name)
                {
                    chestAnimation.Play("open");
                    LoadItemsInChest();
                    manager.LoadMenu(lootMenu);
                    isopen = true;
                }
            }
        }
    }

    private void LoadItemsInChest()
    {
        foreach (RectTransform child in itemList)
        {
            Destroy(child.gameObject);
        }

        int numberOfItems = Random.Range(1, 3);
        List<Equipment> items = new List<Equipment>();

        for (int i = 0; i < numberOfItems; i++)
        {
            items.Add(itemDatabase.GetRandomEquipment());
        }

        GameObject go = (GameObject)Resources.Load("ItemSlotLoot");
        RectTransform prefab = (RectTransform)go.transform;

        foreach (Equipment e in items)
        {
            RectTransform panel = GameObject.Instantiate(prefab);
            panel.Find("Panel/ItemName").GetComponentInChildren<Text>().text = e.Name;
            panel.Find("Panel/ItemSlot").GetComponentInChildren<Text>().text = e.Slot.ToString();
            panel.Find("Panel/ItemStaticID").GetComponentInChildren<Text>().text = e.StaticIDEquipment.ToString();
            Toggle tog = (Toggle)panel.Find("Panel/Take").GetComponent<Toggle>();
            tog.onValueChanged.AddListener((on) => PutInInventory(panel, tog));

            panel.SetParent(itemList);
        }

    }

    private void PutInInventory(RectTransform panel, Toggle sender)
    {
        string selectedID = panel.Find("Panel/ItemStaticID").GetComponentInChildren<Text>().text;

        Destroy(panel.gameObject);

        Equipment e = itemDatabase.GetCopyEquipment(selectedID);

        CurrentPlayer.currentPlayer.PlayerInventory.AddEquipment(e);

        Destroy(GameObject.Find("ItemTooltip(Clone)").gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isopen == true)
        {
            chestAnimation.Play("close");
            manager.CloseMenu();
            isopen = false;
        }
    }
}