using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private footCollider footCollider;

    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float gravitySpeed;
    [SerializeField]
    private float distanceToGround;
    [SerializeField]
    private float playerJumpSpeed;

    private float playerRotX;
    private float playerRotY;

    private CharacterController playerCont;

    private Vector3 playerDirection;

	void Start () {
        playerCont = GetComponent<CharacterController>();
        playerCont.detectCollisions = false;
	}
	
	// Update is called once per frame
	void Update () {
        move();
        rotate();
	}

    public CharacterController PlayerController
    {
        get { return playerCont; }
    }

    void move()
    {
        if (footCollider.IsGrounded)
        {
            float forwardBackward = Input.GetAxis("Vertical") * playerSpeed;
            float leftRight = Input.GetAxis("Horizontal") * playerSpeed;

            playerDirection.y = 0;
            playerDirection = new Vector3(leftRight, 0, forwardBackward);
            playerDirection = transform.TransformDirection(playerDirection);

            if (Input.GetKey(KeyCode.Space))
            {
                playerDirection.y += playerJumpSpeed;
            }
        }else
        if (!footCollider.IsGrounded) 
        {
            playerDirection.y -= gravitySpeed * Time.deltaTime;
        }

        playerCont.Move(playerDirection * Time.deltaTime);        
    }

    void rotate()
    {
        playerRotX += Input.GetAxis("Mouse Y") * 1f;
        playerRotX = Mathf.Clamp(playerRotX, -80,80);

        playerRotY += Input.GetAxis("Mouse X") * 1f;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, playerRotY, transform.localEulerAngles.z);
        playerCamera.localEulerAngles = new Vector3(-(playerRotX), playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
    }

    void OnBecameInvisible()
    {
        Debug.Log("SWAG");
    }
}
