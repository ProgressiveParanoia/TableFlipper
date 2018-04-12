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

    private CharacterController playerCont;

    private Vector3 playerDirection;

    public CharacterController PlayerController
    {
        get { return playerCont; }
    }

    #endregion

        #region Mono
    private void Start ()
    {
        playerCont = GetComponent<CharacterController>();
        playerCont.detectCollisions = false;

        EventTrigger.Entry fwd = new EventTrigger.Entry();
        EventTrigger.Entry fwdUp = new EventTrigger.Entry();

        EventTrigger.Entry bkwd = new EventTrigger.Entry();
        EventTrigger.Entry bkwdUp = new EventTrigger.Entry();

        EventTrigger.Entry left = new EventTrigger.Entry();
        EventTrigger.Entry leftUp = new EventTrigger.Entry();

        EventTrigger.Entry right = new EventTrigger.Entry();
        EventTrigger.Entry rightUp = new EventTrigger.Entry();

        fwd.eventID = EventTriggerType.PointerDown;
        fwdUp.eventID = EventTriggerType.PointerUp;

        bkwd.eventID = EventTriggerType.PointerDown;
        bkwdUp.eventID = EventTriggerType.PointerUp;

        left.eventID = EventTriggerType.PointerDown;
        leftUp.eventID = EventTriggerType.PointerUp;

        right.eventID = EventTriggerType.PointerDown;
        rightUp.eventID = EventTriggerType.PointerUp;

        fwd.callback.AddListener((data) => { OnForwardPressed(data as PointerEventData); });
        fwdUp.callback.AddListener((data) => { OnVerticalKeysNotPressed(data as PointerEventData); });

        bkwd.callback.AddListener((data) => { OnBackwardPressed(data as PointerEventData); });
        bkwdUp.callback.AddListener((data) => { OnVerticalKeysNotPressed(data as PointerEventData); });

        left.callback.AddListener((data) => { OnLeftPressed(data as PointerEventData); });
        leftUp.callback.AddListener((data) => { OnHorizontalKeysNotPressed(data as PointerEventData); });

        right.callback.AddListener((data) => { OnRightPressed(data as PointerEventData); });
        rightUp.callback.AddListener((data) => { OnHorizontalKeysNotPressed(data as PointerEventData); });

        PlayerManager.Instance.PlayerControlsOverlay.ForwardButtonTrigger.triggers.Add(fwd);
        PlayerManager.Instance.PlayerControlsOverlay.ForwardButtonTrigger.triggers.Add(fwdUp);

        PlayerManager.Instance.PlayerControlsOverlay.BackwardButtonTrigger.triggers.Add(bkwd);
        PlayerManager.Instance.PlayerControlsOverlay.BackwardButtonTrigger.triggers.Add(bkwdUp);

        PlayerManager.Instance.PlayerControlsOverlay.LeftButtonTrigger.triggers.Add(left);
        PlayerManager.Instance.PlayerControlsOverlay.LeftButtonTrigger.triggers.Add(leftUp);

        PlayerManager.Instance.PlayerControlsOverlay.RightButtonTrigger.triggers.Add(right);
        PlayerManager.Instance.PlayerControlsOverlay.RightButtonTrigger.triggers.Add(rightUp);
    }
	
	// Update is called once per frame
	private void Update ()
    {
        move();
        rotate();
	}

    #endregion

    #region Player Controller Unity Events
    private void OnForwardPressed(PointerEventData data)
    {
        this.verticalAxisValue = 1;
    }

    private void OnBackwardPressed(PointerEventData data)
    {
        this.verticalAxisValue = -1;
    }
    
    private void OnLeftPressed(PointerEventData data)
    {
        this.horizontalAxisValue = -1;
    }

    private void OnRightPressed(PointerEventData data)
    {
        this.horizontalAxisValue = 1;
    }

    private void OnVerticalKeysNotPressed(PointerEventData data)
    {
        this.verticalAxisValue = 0;
    }

    private void OnHorizontalKeysNotPressed(PointerEventData data)
    {
        this.horizontalAxisValue = 0;
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
        playerRotX = Mathf.Clamp(playerRotX, -80, 80);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, playerRotY, transform.localEulerAngles.z);
        playerCamera.localEulerAngles = new Vector3(-(playerRotX), playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
    }

    void OnBecameInvisible()
    {
        Debug.Log("SWAG");
    }
}
