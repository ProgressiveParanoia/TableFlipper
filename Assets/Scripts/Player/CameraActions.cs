﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowMonsters.Tables;
using UnityEngine.EventSystems;

public class CameraActions : MonoBehaviour {

    #region fields and properties
    private TableManager tableManager;
    private PlayerManager playerManager;

    [SerializeField]
    private float tableRayDistance;
    [SerializeField]
    private float exertedForce;
    private bool hittingTable;

    private Vector3 cameraDirection;
    #endregion

    #region Mono
    private void Start()
    {
        this.tableManager = TableManager.Instance;
        this.playerManager = PlayerManager.Instance;

        //EventTrigger.Entry flip = new EventTrigger.Entry();
        //flip.eventID = EventTriggerType.PointerDown;
        //flip.callback.AddListener((data) => { OnFlipPressed(data as PointerEventData); });

        //this.playerManager.PlayerControlsOverlay.FlipButtonTrigger.triggers.Add(flip);
      //  this.playerManager.PlayerControlsOverlay.FlipButtonEvent.AddListener(this.OnFlipPressed);
    }

    private void OnDestroy()
    {

    }
   
    void Update ()
    {
        this.CheckFront();
    }

    #endregion

    #region UIPlayerController Events Listeners
    
    private void OnFlipPressed(PointerEventData data)
    {
        if (tableManager.ActiveTable == null)
        {
            return;
        }

        Rigidbody activeRigid = tableManager.ActiveTable.GetComponent<Rigidbody>();
        activeRigid.AddForce(transform.forward * exertedForce); 
    }

    #endregion

    private void CheckFront()
    {
        this.RayCastObjects();
    }
    private void RayCastObjects()
    {
        RaycastHit tableRay;

        Debug.DrawRay(transform.position,transform.forward,Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out tableRay, tableRayDistance))
        {
            TableBehaviour table = tableRay.collider.GetComponent<TableBehaviour>() != null ? tableRay.collider.GetComponent<TableBehaviour>() : null;

            if(table == null)
            {
                tableManager.ClearActiveTable();
                return;
            }

            tableManager.SetActiveTable(table);
        }
        else
        {
            tableManager.ClearActiveTable();
        }

    }

    //private IEnumerator HighlightTable()
    //{
    //    if(this.activeTable != null)
    //    {
    //        foreach (Transform renderer in this.activeTable.GetComponentInChildren<Transform>())
    //        {
    //            renderer.GetComponent<MeshRenderer>().material = highlightMat;
    //            yield return null;
    //        }

    //        this.setHighlightCoroutine = null;
    //    }
    //}

    //private IEnumerator SetDefault()
    //{
    //    if (this.activeTable != null)
    //    {
    //        foreach (Transform renderer in this.activeTable.GetComponentInChildren<Transform>())
    //        {
    //            renderer.GetComponent<MeshRenderer>().material = defaultMat;
    //            yield return null;
    //        }
    //        this.activeTable = null;
    //        this.setDefaultHighlightCoroutine = null;
    //    }
    //}

    //private void CheckActiveTable()
    //{
    //    foreach (GameObject table in tables)
    //    {
    //        if (activeTable == table)
    //            table.GetComponent<MeshRenderer>().material = highlightMat;
    //        if(activeTable != table)
    //            table.GetComponent<MeshRenderer>().material = defaultMat;
    //    }
    //}
}
