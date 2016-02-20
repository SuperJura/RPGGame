using UnityEngine;
using System.Collections;
using System;

public class StartingLevelCinematic : MonoBehaviour {

    Transform cameraTransform;
    CameraControl cameraControl;
    Transform cameraParent;
    TalkingBubble talkingBubble;
    bool isFirstMovementFinished;
    bool isSecondMovementFinished;

    void Start () {
        cameraTransform = Camera.main.transform;
        cameraControl = cameraTransform.GetComponent<CameraControl>();
        cameraParent = cameraTransform.parent;
        talkingBubble = GetComponentInParent<TalkingBubble>();
        isFirstMovementFinished = false;
        isSecondMovementFinished = false;
    }

    void OnTriggerEnter(Collider other)
    {
        StartCinematic();
    }

    private void StartCinematic()
    {
        cameraControl.enabled = false;
        cameraTransform.parent = null;
        StartFirstMovement();
        StartCoroutine(StartSecondMovement());
        StartCoroutine(StartFirstSpeech());
    }

    private void StartFirstMovement()
    {
        StartCoroutine(CameraRotation(new Vector3(26, 70, -10)));
        StartCoroutine(CameraTranslation(new Vector3(250, 105, 250)));
    }
    
    private IEnumerator StartSecondMovement()
    {
        while (isFirstMovementFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(CameraRotation(new Vector3(24, 14, 2)));
        StartCoroutine(CameraTranslation(new Vector3(300, 135, 250)));
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

        if (!isFirstMovementFinished)
        {
            isFirstMovementFinished = true;
        }
        else
        {
            isSecondMovementFinished = true;
        }
    }

    private IEnumerator StartFirstSpeech()
    {
        while(isSecondMovementFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        talkingBubble.ShowBubble();
        yield return new WaitForSeconds(5);
        StartCoroutine(StartSecondSpeech());
    }

    private IEnumerator StartSecondSpeech()
    {
        talkingBubble.RemoveBubble(0);
        talkingBubble.ShowBubble("You have quest, folow the road");
        yield return new WaitForSeconds(2);
        talkingBubble.RemoveBubble();
        ReturnControlToPlayer();
    }

    private void ReturnControlToPlayer()
    {
        Debug.Log(cameraParent.name);
        cameraControl.enabled = true;
        cameraTransform.SetParent(cameraParent);
        Destroy(gameObject);
    }
}