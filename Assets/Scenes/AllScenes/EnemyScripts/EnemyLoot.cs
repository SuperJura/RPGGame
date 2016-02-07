using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class EnemyLoot : MonoBehaviour
{
    public InGameMenuMenager manager;
    public InGameMenu lootMenu;
    public Transform itemList;

    private IItemDataBase itemDatabase;
    private EnemyInformation information;
    private PlayerCombat playerCombat;
    private bool canBeTarget;   //ako je neki menu otvoren, igrac nemoze klikat na njega
    private int numberOfLoot;

    void Start()
    {
        itemDatabase = Repository.GetItemDatabaseInstance();
        information = GetComponent<Enemy>().GetEnemyInformation();
        playerCombat = GameObject.Find("PlayerObject").GetComponent<PlayerCombat>();
        manager.OnMenuClosing += manager_OnMenuClosing;
        canBeTarget = true;
        numberOfLoot = information.DropLoot.Count;
    }

    void manager_OnMenuClosing(object sender)
    {
        if (manager.SelectedMenu == ((InGameMenuMenager)sender).SelectedMenu)
        {
            canBeTarget = true;
        }
    }

    public void OnMouseDown()
    {
        //igrac moze lootati ako:
        //  je enemy mrtav
        //  kliknuo s ljevim klikom
        //  blizu je enemya

        if (information.IsDead && Input.GetMouseButtonDown(0))
        {
            if (canBeTarget == true)
            {
                float distance = Vector3.Distance(playerCombat.transform.position, playerCombat.TargetForCombat.position);
                if (distance < 15 && playerCombat.EnemyInfo.IdEnemy == information.IdEnemy)
                {
                    LoadItemsInLootMenu();
                }
            }
        }
    }

    private void LoadItemsInLootMenu()
    {
        foreach (RectTransform child in itemList)
        {
            Destroy(child.gameObject);
        }

        GameObject go = (GameObject)Resources.Load("ItemSlotLoot");
        RectTransform prefab = (RectTransform)go.transform;

        foreach (Equipment e in information.DropLoot)
        {
            RectTransform panel = GameObject.Instantiate(prefab);
            panel.Find("Panel/ItemName").GetComponentInChildren<Text>().text = e.Name;
            panel.Find("Panel/ItemSlot").GetComponentInChildren<Text>().text = e.Slot.ToString();
            panel.Find("Panel/ItemStaticID").GetComponentInChildren<Text>().text = e.StaticIDEquipment.ToString();
            Toggle tog = panel.Find("Panel/Take").GetComponent<Toggle>();
            tog.onValueChanged.AddListener((on) => PutInInventory(panel, tog));

            panel.SetParent(itemList);
        }

        manager.LoadMenu(lootMenu);
        canBeTarget = false;
    }

    private void PutInInventory(RectTransform panel, Toggle sender)
    {
        string staticID = panel.Find("Panel/ItemStaticID").GetComponentInChildren<Text>().text;

        Destroy(panel.gameObject);

        Equipment e = itemDatabase.GetCopyEquipment(staticID);

        CurrentPlayer.currentPlayer.PlayerInventory.AddEquipment(e);
        information.RemoveFromDropLoot(staticID);

        Destroy(GameObject.Find("ItemTooltip(Clone)").gameObject);
        numberOfLoot--;
        if (numberOfLoot == 0)  //ako vise nema nicega za lootanje, disable skriptu
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
