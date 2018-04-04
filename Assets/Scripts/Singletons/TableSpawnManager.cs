using System;
using System.Collections;
using System.Collections.Generic;
using ShadowMonsters.Utilities;
using UnityEngine;
namespace ShadowMonsters.Tables
{
    public class TableSpawnManager : ASubGameManager
    {
        #region fields and properties
        private const string tablePrefabName = "Prefabs/Ingame/Table/Table_Flip";

        private Transform moveableObjectsParent;
        private Transform tableSpawnCollectionParent;

        private List<TableBehaviour> tableCollection;
        private List<TableSpawnObject> tableSpawnCollection;

        public event Action HideSpawnPoints;
        public event Action ShowSpawnPoints;
        #endregion

        #region implemented AGameManager methods
        public override void Setup()
        {
            MonoUtility.Instance.StartCoroutine(this.SetupRoutine());
        }

        protected override IEnumerator SetupRoutine()
        {
            tableCollection = new List<TableBehaviour>();
            tableSpawnCollection = new List<TableSpawnObject>();

            tableSpawnCollection.AddRange(GameObject.FindObjectsOfType<TableSpawnObject>());

            yield return null;
            foreach (TableSpawnObject spawn in tableSpawnCollection)
            {
                spawn.Setup();
                GameObject t = GameObject.Instantiate(Resources.Load(tablePrefabName) as GameObject);
                TableBehaviour table = t.GetComponent<TableBehaviour>();
                table.transform.position = spawn.GetWorldPosition;
                tableCollection.Add(table);

                
                yield return null;
            }

            if(HideSpawnPoints != null)
            HideSpawnPoints();
        }
#endregion
    }
}
