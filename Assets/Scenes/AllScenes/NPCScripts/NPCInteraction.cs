using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(NPCText))]
public class NPCInteraction : MonoBehaviour {

    private bool talking;
    private NPCText npcTextData;

	// Use this for initialization
	void Start () {
        npcTextData = GetComponent<NPCText>();
        talking = false;
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F) && talking == false)
            {
                Vector3 targetLook = other.transform.position;
                targetLook.y = this.transform.position.y;
                this.transform.LookAt(targetLook);

                GameObject go = new GameObject("txtBubble");

                TextMesh t = go.AddComponent<TextMesh>();
                FacingCamera fc = go.AddComponent<FacingCamera>();
                fc.m_Camera = Camera.main;

                t.text = npcTextData.HelloText;
                t.anchor = TextAnchor.UpperCenter;
                t.fontSize = 200;

                go.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
                go.transform.SetParent(this.transform);
                go.transform.position = this.transform.position + new Vector3(0, 5, 0);

                talking = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject,1f);
            }
            talking = false;
        }
    }
}
