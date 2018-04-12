using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIPlayerController : MonoBehaviour
{
    #region fields and properties
   
    public EventTrigger FlipButtonTrigger;
    public EventTrigger ForwardButtonTrigger;
    public EventTrigger BackwardButtonTrigger;
    public EventTrigger LeftButtonTrigger;
    public EventTrigger RightButtonTrigger;
  
    #endregion

    public void Setup()
    {
     
    }

    #region Mono

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
    
}
