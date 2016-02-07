using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class EnemyInteraction : MonoBehaviour{

    private PlayerCombat playerCombat;
    private Renderer myRenderer;
    private Transform myTransform;

    private Color initialColor;
    private Vector3 initialSize;
    
    private Color mouseOverColor;  //kada prede misem preko njega
    private Vector3 mouseOverSize;

    void Start()
    {
        playerCombat= GameObject.Find("PlayerObject").GetComponent<PlayerCombat>();
        myRenderer = this.GetComponent<Renderer>();
        myTransform = transform;
        initialSize = myTransform.localScale;
        initialColor = myRenderer.material.color;

        mouseOverColor = CreateLigherColor(initialColor);
        mouseOverSize = new Vector3(initialSize.x + 0.1f, initialSize.y + 0.1f, initialSize.z + 0.1f);
    }

    void playerCombat_OnTargetHealthChanged(EnemyInformation info)
    {
        SetHealthOnHeatlhtBar(info);
    }

    private Color CreateLigherColor(Color initialColor)
    {
        float r = initialColor.r;
        float g = initialColor.g;
        float b = initialColor.b;

        r += 0.2f;
        b += 0.2f;

        return new Color(r, g, b);
    }

    void OnMouseDown()
    {
        if (playerCombat.TargetForCombat == this.transform)
        {
           return;  //ako oznacava enemya koji je vec oznacen, zavrsi metodu.
        }

        if (playerCombat.TargetForCombat != null)
        {
            RemoveHealthBar();
        }
        playerCombat.TargetForCombat = this.transform;

        DisplayHealthBar();
    }

    private void DisplayHealthBar()
    {
        playerCombat.OnTargetHealthChanged += playerCombat_OnTargetHealthChanged;
        GameObject go = (GameObject)Resources.Load("HealthBarCanvas");
        RectTransform prefab = (RectTransform)go.transform;

        RectTransform healthBar = GameObject.Instantiate(prefab);

        Canvas healthBarCanvas = (Canvas)healthBar.GetComponent<Canvas>();

        healthBarCanvas.worldCamera = Camera.main;
        healthBarCanvas.GetComponent<FacingCamera>().m_Camera = Camera.main;

        healthBar.SetParent(this.transform, false);
        healthBarCanvas.transform.localPosition.Set(0f, 10f, 0f);

        SetHealthOnHeatlhtBar(playerCombat.EnemyInfo);
    }

    private void SetHealthOnHeatlhtBar(EnemyInformation info)
    {
        Canvas enCanvas = playerCombat.TargetForCombat.Find("HealthBarCanvas(Clone)").GetComponent<Canvas>();
        Image healthImage = enCanvas.transform.Find("HealthBackGround/Health").GetComponent<Image>();

        healthImage.fillAmount =(float)info.Health / (float)info.MaxHealth;
    }

    private void RemoveHealthBar()
    {
        playerCombat.OnTargetHealthChanged -= playerCombat_OnTargetHealthChanged;
        Destroy(playerCombat.TargetForCombat.Find("HealthBarCanvas(Clone)").gameObject);
    }

    void OnMouseEnter()
    {
        myRenderer.material.color = mouseOverColor;
        myTransform.localScale = mouseOverSize;
    }

    void OnMouseExit()
    {
        myRenderer.material.color = initialColor;
        myTransform.localScale = initialSize;
    }
}