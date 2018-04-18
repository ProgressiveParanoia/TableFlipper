using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    #region Fields and Properties

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

    //for mobile inputs
    private float horizontalAxisValue;
    private float verticalAxisValue;

    private float rotateXValue;
    private float rotateYValue;

    private CharacterController playerCont;

    private Vector3 playerDirection;

    public CharacterController PlayerController
    {
        get { return playerCont; }
    }

    #endregion

    #region Mono
    private void Awake()
    {
        playerCont = GetComponent<CharacterController>();
        playerCont.detectCollisions = false;
    }

    private void Start ()
    {
        UIPlayerController.Instance.RotateCamera += this.OnRotateCamera;
        UIPlayerController.Instance.MoveForward += this.OnForwardPressed;
        UIPlayerController.Instance.MoveBackward += this.OnBackwardPressed;
        UIPlayerController.Instance.MoveRight += this.OnRightPressed;
        UIPlayerController.Instance.MoveLeft += this.OnLeftPressed;

        UIPlayerController.Instance.MoveVerticalKeysUp += this.OnVerticalKeysNotPressed;
        UIPlayerController.Instance.MoveHorizontalKeysUp += this.OnHorizontalKeysNotPressed;
    }
	
    private void OnDestroy()
    {
        UIPlayerController.Instance.RotateCamera -= this.OnRotateCamera;
        UIPlayerController.Instance.MoveForward -= this.OnForwardPressed;
        UIPlayerController.Instance.MoveBackward -= this.OnBackwardPressed;
        UIPlayerController.Instance.MoveRight -= this.OnRightPressed;
        UIPlayerController.Instance.MoveLeft -= this.OnLeftPressed;

        UIPlayerController.Instance.MoveVerticalKeysUp -= this.OnVerticalKeysNotPressed;
        UIPlayerController.Instance.MoveHorizontalKeysUp -= this.OnHorizontalKeysNotPressed;
    }

	// Update is called once per frame
	private void Update ()
    {
        move();
        rotate();
	}

    #endregion

    #region Player Controller Unity Events
    private void OnForwardPressed()
    {
        this.verticalAxisValue = 1;
    }

    private void OnBackwardPressed()
    {
        this.verticalAxisValue = -1;
    }
    
    private void OnLeftPressed()
    {
        this.horizontalAxisValue = -1;
    }

    private void OnRightPressed()
    {
        this.horizontalAxisValue = 1;
    }

    private void OnVerticalKeysNotPressed()
    {
        this.verticalAxisValue = 0;
    }

    private void OnHorizontalKeysNotPressed()
    {
        this.horizontalAxisValue = 0;
    }

    private void OnRotateCamera(float x, float y)
    {
        this.rotateXValue = x;
        this.rotateYValue = y;
    }
    #endregion

    //TODO: ADD PROPER EVENT-DRIVEN INPUTS. TRY NOT TO RELY ON UPDATE SO MUCH.
    void move()
    {
        if (footCollider.IsGrounded)
        {

            float forwardBackward = 0;
            float leftRight = 0;
            
            #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            forwardBackward = Input.GetAxis("Vertical") * playerSpeed;
            leftRight = Input.GetAxis("Horizontal") * playerSpeed;
            #endif

            #if UNITY_ANDROID
            forwardBackward = verticalAxisValue * playerSpeed;
            leftRight = horizontalAxisValue * playerSpeed;     
            #endif

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
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        playerRotX += Input.GetAxis("Mouse Y") * 1f;
        playerRotY += Input.GetAxis("Mouse X") * 1f;
        #endif

        #if UNITY_ANDROID
        playerRotX += rotateYValue * 1f;
        playerRotY += rotateXValue * 1f;
        #endif

        playerRotX = Mathf.Clamp(playerRotX, -80, 80);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, playerRotY, transform.localEulerAngles.z);
        playerCamera.localEulerAngles = new Vector3(-(playerRotX), playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
    }

    void OnBecameInvisible()
    {
        Debug.Log("SWAG");
    }
}
