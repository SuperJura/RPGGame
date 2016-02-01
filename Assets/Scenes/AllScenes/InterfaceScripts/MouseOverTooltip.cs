using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private IItemDataBase itemDatabase;
    private Canvas parent;

    RectTransform panel;

	// Use this for initialization
	void Start () {
        panel = GetComponent<RectTransform>();
        itemDatabase = Repository.GetItemDatabaseInstance();
        parent = GameObject.Find("Canvas").GetComponent<Canvas>();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        string id = panel.Find("Panel/ItemStaticID").GetComponentInChildren<Text>().text;
        Equipment e = itemDatabase.GetEquipment(id);

        GameObject go = (GameObject)Resources.Load("ItemToolTip");

        RectTransform prefab = (RectTransform)GameObject.Instantiate(go.transform);

        prefab.Find("ItemName").GetComponentInChildren<Text>().text = e.Name;
        prefab.Find("txts/txtHealth").GetComponentInChildren<Text>().text = e.Health.ToString();
        prefab.Find("txts/txtMana").GetComponentInChildren<Text>().text = e.Mana.ToString();
        prefab.Find("txts/txtMoveSpeed").GetComponentInChildren<Text>().text = e.MoveSpeed.ToString();
        prefab.Find("txts/txtPhysDmg").GetComponentInChildren<Text>().text = e.PhysDMGMultiplication.ToString();
        prefab.Find("txts/txtMagicDmg").GetComponentInChildren<Text>().text = e.MagicDMGMultiplication.ToString();

        prefab.SetParent(parent.gameObject.transform);
        prefab.anchoredPosition = new Vector2(0, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(parent.gameObject.transform.Find("ItemTooltip(Clone)").gameObject);
    }
}
