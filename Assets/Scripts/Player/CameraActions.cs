using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowMonsters.Tables;

public class CameraActions : MonoBehaviour {

    #region fields and properties
    private TableManager tableManager;

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
    }
    #endregion

    void Update ()
    {
        this.CheckFront();
    }

    private void CheckFront()
    {
        this.RayCastObjects();

        if (/*this.hittingTable &&*/ tableManager.ActiveTable != null)
        {
            Rigidbody activeRigid = tableManager.ActiveTable.GetComponent<Rigidbody>();

            #if UNITY_EDITOR || UNITY_STANDALONE_WIN
                if (Input.GetKeyDown(KeyCode.T))
                {
                    activeRigid.AddForce(transform.forward * exertedForce);
               
                }
            #endif
        }
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
