using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(TalkingBubble))]
public class NPCInteraction : MonoBehaviour {

    private bool talking;
    private TalkingBubble talkinBubble;
    private Collider player;
    private bool playerClose;

    void Start () {
        talkinBubble = GetComponent<TalkingBubble>();
        talking = false;
        playerClose = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other;
            playerClose = true;
        }
    }

    void OnMouseDown()
    {
        if (talking == false && playerClose == true)
        {
            Vector3 targetLook = player.transform.position;
            targetLook.y = this.transform.position.y;
            this.transform.LookAt(targetLook);

            talkinBubble.ShowBubble();
            talking = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            talkinBubble.RemoveBubble();
            talking = false;
            playerClose = false;
        }
    }
}