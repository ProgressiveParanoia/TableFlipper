using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowMonsters.Tables
{
    public class TableBehaviour : MonoBehaviour
    {
        private TableMeshBehavior tableMeshController;

        public TableMeshBehavior TableMeshController
        {
            get { return tableMeshController; }
        }
        #region Mono
        private void Start()
        {
            tableMeshController = this.GetComponentInChildren<TableMeshBehavior>();   
        }

        #endregion
    }
}
