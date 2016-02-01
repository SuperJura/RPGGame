using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class AbilityMouseOverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    private IAbilityDatabase abilityDatabase;
    private RectTransform parent;
    private Button abilityBtn;    //recttransform tooltipa

    void Start () {
        abilityDatabase = Repository.GetAbilityDatabaseInstance();
        abilityBtn = GetComponent<Button>();
        parent = GameObject.Find("Canvas/CharAbilitys").GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        abilityBtn.transform.Find("AbilityStaticID").GetComponent<Button>();
        int staticID = int.Parse(abilityBtn.transform.Find("AbilityStaticID").GetComponent<Text>().text);
        Debug.Log(staticID);

        Ability ab = abilityDatabase.GetAbility(staticID);

        GameObject go = (GameObject)Resources.Load("AbilityTooltip");
        RectTransform prefab = (RectTransform)Instantiate(go.transform);

        prefab.Find("AbilityName").GetComponent<Text>().text = ab.Name;
        prefab.Find("AbilityDescription").GetComponent<Text>().text = ab.Description;
        prefab.Find("txts/txtDamage").GetComponent<Text>().text = ab.Damage.ToString();
        prefab.Find("txts/txtCost").GetComponent<Text>().text = ab.ManaCost.ToString();
        prefab.Find("txts/txtRange").GetComponent<Text>().text = ab.Range.ToString();
        prefab.Find("txts/txtCooldown").GetComponent<Text>().text = ab.Cooldown.ToString();

        prefab.SetParent(parent.gameObject.transform);
        prefab.anchoredPosition = new Vector2(0, 120);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(parent.gameObject.transform.Find("AbilityTooltip(Clone)").gameObject);
    }
}
