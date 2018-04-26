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
    private RectTransform rotationalLookContainer;
    [SerializeField]
    private RectTransform rotationalLookButtonTransform;
    [SerializeField]
    private EventTrigger rotationalLookControllerTrigger;
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
        Debug.LogError("Rotational controller:"+rotationalLookContainer.rect.width);
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

        this.rotationalLookControllerTrigger.triggers.Add(lookRotation);
        this.rotationalLookControllerTrigger.triggers.Add(draggingRotation);
        this.rotationalLookControllerTrigger.triggers.Add(stopDrag);
        
        this.cameraControllerAxis = new Vector2(rotationalLookContainer.rect.width / 2, rotationalLookContainer.rect.height / 2);
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
        Debug.LogError("curr object pos:"+rotationalLookContainer.position + " rect vector equivalent:"+rotationalLookButtonTransform.anchoredPosition + "its name:"+ rotationalLookButtonTransform.name);
        Debug.LogError("curr point pos:"+pointerData.position);
    }

    private void OnDragRotation(PointerEventData pointerData)
    {
        float maxCameraContainerX = rotationalLookContainer.InverseTransformPoint(pointerData.position.x, pointerData.position.y, 0).x;
        float maxCameraContainerY = rotationalLookContainer.InverseTransformPoint(pointerData.position.x, pointerData.position.y, 0).y;

        this.cameraControllerPosition.x = maxCameraContainerX;
        this.cameraControllerPosition.y = maxCameraContainerY;
        this.cameraControllerPosition.x = Mathf.Clamp(this.cameraControllerPosition.x, -this.cameraControllerAxis.x, this.cameraControllerAxis.x);
        this.cameraControllerPosition.y = Mathf.Clamp(this.cameraControllerPosition.y, -this.cameraControllerAxis.y, this.cameraControllerAxis.y);

        float normalizedX = MathUtil.Normalize(cameraControllerPosition.x, this.cameraControllerAxis.x);
        float normalizedY = MathUtil.Normalize(cameraControllerPosition.y, this.cameraControllerAxis.y);

        this.rotationalLookButtonTransform.anchoredPosition = this.cameraControllerPosition;
        Debug.LogError("do the thing");
        if(RotateCamera != null)
        {
            RotateCamera(normalizedX, normalizedY);
        }
    }

    private void OnDragEnd(PointerEventData pointerData)
    {
        this.cameraControllerPosition.x = 0;
        this.cameraControllerPosition.y = 0;

        this.rotationalLookButtonTransform.anchoredPosition = this.cameraControllerPosition;

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
