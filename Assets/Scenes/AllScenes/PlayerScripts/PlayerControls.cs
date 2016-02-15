using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private Transform myTransform;
    private Transform cameraTransform;
    private bool isGrounded = false;
    private Vector3 gravity = new Vector3(0, -5, 0);
    private Vector3 jumpVelocity;
    private bool jumping = false;   //ako igrac zeli skociti (znaci da ne pada)

    public float rotationSpeed = 200;

    void Start () {
        myTransform = GetComponent<Transform>();
        cameraTransform = Camera.main.transform;
    }

    void Update () {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myTransform.position += myTransform.forward * Time.deltaTime * CurrentPlayer.currentPlayer.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            myTransform.position -= myTransform.forward * Time.deltaTime * CurrentPlayer.currentPlayer.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            myTransform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            myTransform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            isGrounded = false;
            jumping = true;
            jumpVelocity = new Vector3(0, CurrentPlayer.currentPlayer.JumpForce, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            myTransform.position -= myTransform.right * Time.deltaTime * CurrentPlayer.currentPlayer.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            myTransform.position += myTransform.right * Time.deltaTime * CurrentPlayer.currentPlayer.MoveSpeed;
        }

        if (isGrounded == false)
        {
            myTransform.position = myTransform.position + Time.deltaTime * jumpVelocity;
            jumpVelocity = jumpVelocity + Time.deltaTime * gravity * 2;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
        jumping = false;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Floor")
        {
            if (!jumping)
            {
                isGrounded = false;
                jumpVelocity = new Vector3(0, 0.01f, 0);
            }
        }
    }
}