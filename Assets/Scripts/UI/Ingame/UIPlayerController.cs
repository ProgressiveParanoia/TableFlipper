using System;
using System.Collections;
using System.Collections.Generic;
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
    float testRotValueX;
    float testRotValueY;
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

        lookRotation.eventID = EventTriggerType.BeginDrag;
        draggingRotation.eventID = EventTriggerType.Drag;

        lookRotation.callback.AddListener((data) => { OnRotatePlayer(data as PointerEventData); });
        draggingRotation.callback.AddListener((data) => { OnDragRotation(data as PointerEventData); });

        this.rotationalControllerTrigger.triggers.Add(lookRotation);
        this.rotationalControllerTrigger.triggers.Add(draggingRotation);
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

    public void OnDragRotation(PointerEventData pointerData)
    {
        //TODO: MATH UTILITY CLASS 
      //  Debug.LogError("container anchored position:" + rotationalContainer.anchoredPosition + "pointer val" + contain.InverseTransformPoint(pointerData.position.x, pointerData.position.y, 0));
        float edgeX = rotationalContainer.InverseTransformPoint(pointerData.position.x, pointerData.position.y, 0).x;
        float edgeYValue = rotationalContainer.InverseTransformPoint(pointerData.position.x, pointerData.position.y, 0).y;

        float testX = rotationalContainer.rect.width / 2;
        float testY = rotationalContainer.rect.height / 2;
   
        testRotValueX = edgeX;
        testRotValueY = edgeYValue;
        testRotValueX = Mathf.Clamp(testRotValueX, -testX, testX);
        testRotValueY = Mathf.Clamp(testRotValueY, -testY, testY);
        
        float normalizedX = testRotValueX / testX;
        float normalizedY = testRotValueY / testY;
       
        rotationalButtonTransform.anchoredPosition = new Vector2(testRotValueX, testRotValueY);
       
        if(RotateCamera != null)
        {
            RotateCamera(normalizedX, normalizedY);
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
