using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    Transform myTransform;
    Transform parentToReturn;
    Transform parentDragging;
    AbilityMouseOverTooltip tooltip;
    RectTransform placeholder;

    void Start () {
        myTransform = transform;
        parentToReturn = myTransform.parent;
        parentDragging = myTransform.parent.parent;
        tooltip = GetComponent<AbilityMouseOverTooltip>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        myTransform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject go = new GameObject();
        placeholder = go.AddComponent<RectTransform>();
        placeholder.SetParent(parentToReturn);
        placeholder.transform.SetSiblingIndex(myTransform.GetSiblingIndex());
        placeholder.transform.localScale = new Vector3(10, 10, 0);

        myTransform.SetParent(parentDragging);
        tooltip.DestroyTooltip();
        tooltip.enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        myTransform.SetParent(parentToReturn);
        myTransform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        Destroy(placeholder.gameObject);
        tooltip.enabled = true;
    }
}