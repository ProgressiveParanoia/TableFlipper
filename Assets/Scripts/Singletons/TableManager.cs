﻿using System;
using System.Collections;
using System.Collections.Generic;
using ShadowMonsters.Utilities;
using UnityEngine;

namespace ShadowMonsters.Tables
{
    public class TableManager : AGameManager
    {
        #region fields and properties
        private TableSpawnManager tableSpawnManager;

        private TableBehaviour activeTable;

        public event Action<string> HighlightObject;
        public event Action<string> ClearHighlights;

        public TableSpawnManager TableSpawnManager { get { return tableSpawnManager; } }
        #endregion

        public override void Setup()
        {
            MonoUtility.Instance.StartCoroutine(SetupRoutine());
        }

        protected override IEnumerator SetupRoutine()
        {
            yield return null;
        }
        #region Mono
        private void Awake()
        {
            instance = this.GetComponent<TableManager>();
            tableSpawnManager = new TableSpawnManager();
            tableSpawnManager.Setup();
        }
        #endregion

        #region Singleton Implementation

        private static TableManager instance;
        public static TableManager Instance
        {
            get
            {   return instance;    }
        }
        #endregion

        public TableBehaviour ActiveTable
        {
            get { return this.activeTable; }
        }

        public void SetActiveTable(TableBehaviour activeTable)
        {
            //TODO: CREATE AN INSTANCE ID MANAGER FOR NETWORKING PURPOSES
            if (this.activeTable == null)
            {
                this.activeTable = activeTable;
                string instanceID = this.activeTable.GetInstanceID().ToString();

                if (HighlightObject != null)
                    HighlightObject(instanceID);
            }
        }
        
        public void ClearActiveTable()
        {
            if(this.activeTable != null)
            {
                string instanceID = this.activeTable.GetInstanceID().ToString();
                if (ClearHighlights != null)
                    ClearHighlights(instanceID);

                this.activeTable = null;

            }
        }

        public void CheckActiveTable()
        {

        }
    }
}
