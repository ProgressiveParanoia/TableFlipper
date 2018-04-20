using System;
using System.Collections;
using System.Collections.Generic;
using ShadowMonsters.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIPlayerController : MonoBehaviour
{

    #region fields and properties
    [SerializeField]
    private RectTransform rotationalContainer;
    [SerializeField]
    private RectTransform rotationalButtonTransform;
    [SerializeField]
    private EventTrigger rotationalControllerTrigger;
    [SerializeField]
    private EventTrigger FlipButtonTrigger;
    [SerializeField]
    private EventTrigger ForwardButtonTrigger;
    [SerializeField]
    private EventTrigger BackwardButtonTrigger;
    [SerializeField]
    private EventTrigger LeftButtonTrigger;
    [SerializeField]
    private EventTrigger RightButtonTrigger;

    private Vector2 cameraControllerPosition;
    private Vector2 cameraControllerAxis;

    public event Action MoveForward;
    public event Action MoveBackward;
    public event Action MoveRight;
    public event Action MoveLeft;

    public event Action MoveVerticalKeysUp;
    public event Action MoveHorizontalKeysUp;

    public event Action<float, float> RotateCamera;
    #endregion

    #region singleton implementation
    private static UIPlayerController instance;
    public static UIPlayerController Instance
    {
        get { return instance; }
    }
    #endregion

    #region Mono

    private void Awake()
    {
        Debug.LogError("Rotational controller:"+rotationalContainer.rect.width);
        instance = this.GetComponent<UIPlayerController>();
    }

    private void Start()
    {
        EventTrigger.Entry lookRotation = new EventTrigger.Entry();
        EventTrigger.Entry draggingRotation = new EventTrigger.Entry();
        EventTrigger.Entry stopDrag = new EventTrigger.Entry();

        lookRotation.eventID = EventTriggerType.BeginDrag;
        draggingRotation.eventID = EventTriggerType.Drag;
        stopDrag.eventID = EventTriggerType.EndDrag;

        lookRotation.callback.AddListener((data) => { OnRotatePlayer(data as PointerEventData); });
        draggingRotation.callback.AddListener((data) => { OnDragRotation(data as PointerEventData); });
        stopDrag.callback.AddListener((data) => { OnDragEnd(data as PointerEventData); });

        this.rotationalControllerTrigger.triggers.Add(lookRotation);
        this.rotationalControllerTrigger.triggers.Add(draggingRotation);
        this.rotationalControllerTrigger.triggers.Add(stopDrag);

        this.cameraControllerAxis = new Vector2(rotationalContainer.rect.width / 2, rotationalContainer.rect.height / 2);
    }

    private void Update()
    {
        //for debug purposes
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.T))
        {
           // this.FlipButtonTrigger
            //OnFlipClicked();
        }
        #endif
    }
    #endregion

    #region Event Callbacks
    public void OnRotatePlayer(PointerEventData pointerData)
    {
        
        Debug.LogError("curr object pos:"+rotationalContainer.position + " rect vector equivalent:"+rotationalButtonTransform.anchoredPosition + "its name:"+ rotationalButtonTransform.name);
        Debug.LogError("curr point pos:"+pointerData.position);
    }

    private void OnDragRotation(PointerEventData pointerData)
    {
        float maxCameraContainerX = rotationalContainer.InverseTransformPoint(pointerData.position.x, pointerData.position.y, 0).x;
        float maxCameraContainerY = rotationalContainer.InverseTransformPoint(pointerData.position.x, pointerData.position.y, 0).y;

        this.cameraControllerPosition.x = maxCameraContainerX;
        this.cameraControllerPosition.y = maxCameraContainerY;
        this.cameraControllerPosition.x = Mathf.Clamp(this.cameraControllerPosition.x, -this.cameraControllerAxis.x, this.cameraControllerAxis.x);
        this.cameraControllerPosition.y = Mathf.Clamp(this.cameraControllerPosition.y, -this.cameraControllerAxis.y, this.cameraControllerAxis.y);

        float normalizedX = MathUtil.Normalize(cameraControllerPosition.x, this.cameraControllerAxis.x);
        float normalizedY = MathUtil.Normalize(cameraControllerPosition.y, this.cameraControllerAxis.y);

        this.rotationalButtonTransform.anchoredPosition = this.cameraControllerPosition;
       
        if(RotateCamera != null)
        {
            RotateCamera(normalizedX, normalizedY);
        }
    }

    private void OnDragEnd(PointerEventData pointerData)
    {
        this.cameraControllerPosition.x = 0;
        this.cameraControllerPosition.y = 0;

        this.rotationalButtonTransform.anchoredPosition = this.cameraControllerPosition;

        if(RotateCamera != null)
        {
            RotateCamera(0,0);
        }
    }

    public void OnMoveForwardPressed()
    {
        if(this.MoveForward != null)
        {
            this.MoveForward();
        }
    }
    
    public void OnMoveBackPressed()
    {
        if(this.MoveBackward != null)
        {
            this.MoveBackward();
        }
    }

    public void OnMoveLeftPressed()
    {
        if(this.MoveLeft != null)
        {
            this.MoveLeft();
        }
    }

    public void OnMoveRightPressed()
    {
        if(this.MoveRight != null)
        {
            this.MoveRight();
        }
    }

    public void OnVerticalKeysUp()
    {
        if(this.MoveVerticalKeysUp != null)
        {
            this.MoveVerticalKeysUp();
        }
    }

    public void OnHorizontalKeysUp()
    {
        if(this.MoveHorizontalKeysUp != null)
        {
            this.MoveHorizontalKeysUp();
        }
    }
    #endregion

}
