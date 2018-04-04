using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSingleton : MonoBehaviour {

    public event Action<string> SomeEvent;

    #region singleton implementation
    private static SenderSingleton instance;

    public static SenderSingleton Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    #region Mono
    private void Awake()
    {
        instance = this.GetComponent<SenderSingleton>();   
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
           //SomeEvent.GetInvocationList()
            SomeEvent("It works!");
        }
    }
    #endregion
}
