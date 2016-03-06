using System;
using UnityEngine;

[RequireComponent(typeof(NPCText))]
class TalkingBubble : MonoBehaviour
{
    NPCText npcTextData;
    void Start()
    {
        npcTextData = transform.GetComponent<NPCText>();
    }

    public void ShowBubble()
    {
        ShowBubble(npcTextData.HelloText);
    }

    public void ShowBubble(string text)
    {
        GameObject go = new GameObject("txtBubble");

        TextMesh t = go.AddComponent<TextMesh>();
        FacingCamera fc = go.AddComponent<FacingCamera>();
        fc.m_Camera = Camera.main;

        t.text = text;
        t.anchor = TextAnchor.UpperCenter;
        t.characterSize = npcTextData.CharacterSize;
        t.fontSize = 200;

        go.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        go.transform.SetParent(transform);
        float meshHeight = GetComponent<Renderer>().bounds.size.y;
        go.transform.position = transform.position + new Vector3(0, meshHeight + 1, 0);
    }

    public void RemoveBubble()
    {
        RemoveBubble(1);
    }

    public void RemoveBubble(float delay)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "txtBubble")
            {
                Destroy(transform.GetChild(i).gameObject, delay);
            }
        }
    }
}