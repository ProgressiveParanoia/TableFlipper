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
    #endregion


    public void Setup()
    {
     
    }

    #region Mono

    private void Awake()
    {
        
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
