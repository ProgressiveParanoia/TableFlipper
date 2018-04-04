using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverBehaviour : MonoBehaviour
{
    #region Mono
    private void Start()
    {
        SenderSingleton.Instance.SomeEvent += this.OnSomeEvent;
    }
    private void OnDestroy()
    {
        SenderSingleton.Instance.SomeEvent -= this.OnSomeEvent;
    }
    #endregion

    private void OnSomeEvent(string msg)
    {
        Debug.LogError("Received a message: "+ msg);
    }
}
