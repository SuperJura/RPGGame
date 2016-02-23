using UnityEngine;
using System.Collections;
using System;

public class StartingLevelCinematic : MonoBehaviour {

    Transform cameraTransform;
    CameraControl cameraControl;
    PlayerControls playerControls;
    Transform cameraParent;
    TalkingBubble talkingBubble;
    CanvasGroup canvas;

    void Start () {
        cameraTransform = Camera.main.transform;
        cameraControl = cameraTransform.GetComponent<CameraControl>();
        cameraParent = cameraTransform.parent;
        playerControls = cameraParent.GetComponent<PlayerControls>();
        talkingBubble = GetComponentInParent<TalkingBubble>();
        canvas = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
    }

    void OnTriggerEnter(Collider other)
    {
        cameraControl.enabled = false;
        playerControls.enabled = false;
        cameraTransform.parent = null;
        canvas.alpha = 0;
        StartCoroutine(StartCinematic());
    }

    private IEnumerator StartCinematic()
    {
        yield return StartCoroutine(StartFirstMovement());
        yield return StartCoroutine(StartSecondMovement());
        yield return StartCoroutine(StartFirstSpeech());
        yield return StartCoroutine(StartSecondSpeech());
        ReturnControlToPlayer();
    }

    private IEnumerator StartFirstMovement()
    {
        StartCoroutine(CameraRotation(new Vector3(26, 70, -10)));
        yield return StartCoroutine(CameraTranslation(new Vector3(250, 105, 250)));
    }
    
    private IEnumerator StartSecondMovement()
    {
        StartCoroutine(CameraRotation(new Vector3(24, 14, 2)));
        yield return StartCoroutine(CameraTranslation(new Vector3(300, 135, 250)));
    }

    private IEnumerator CameraRotation(Vector3 targetAngle)
    {
        Vector3 currentAngle = cameraTransform.rotation.eulerAngles;
        while (Mathf.RoundToInt(currentAngle.x) != targetAngle.x)
        {
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

            cameraTransform.eulerAngles = currentAngle;
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log(cameraTransform.eulerAngles);
    }

    private IEnumerator CameraTranslation(Vector3 targetPosition)
    {
        Vector3 currentPosition = cameraTransform.position;
        while (Mathf.RoundToInt(currentPosition.x) != targetPosition.x)
        {
            currentPosition = new Vector3(
                Mathf.Lerp(currentPosition.x, targetPosition.x, Time.deltaTime),
                Mathf.Lerp(currentPosition.y, targetPosition.y, Time.deltaTime),
                Mathf.Lerp(currentPosition.z, targetPosition.z, Time.deltaTime));

            cameraTransform.position = currentPosition;
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log(cameraTransform.position);
    }

    private IEnumerator StartFirstSpeech()
    {
        talkingBubble.ShowBubble();
        yield return new WaitForSeconds(2);
    }

    private IEnumerator StartSecondSpeech()
    {
        talkingBubble.RemoveBubble(0);
        talkingBubble.ShowBubble("You have quest, folow the road");
        yield return new WaitForSeconds(2);
        talkingBubble.RemoveBubble(0);
    }

    private void ReturnControlToPlayer()
    {
        cameraControl.enabled = true;
        playerControls.enabled = true;
        cameraTransform.SetParent(cameraParent);
        canvas.alpha = 1;
        Destroy(gameObject);
    }
}