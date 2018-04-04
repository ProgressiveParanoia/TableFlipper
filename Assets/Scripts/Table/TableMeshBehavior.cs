using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowMonsters.Tables
{
    public class TableMeshBehavior : MonoBehaviour
    {
        [SerializeField]
        private Material defaultMat;
        [SerializeField]
        private Material highlightMat;

        [SerializeField]
        private MeshRenderer[] tableMeshes;

        private Coroutine defaultTableHiglightRoutine;
        private Coroutine tableHiglightRoutine;

        #region Mono
        private void Start()
        {
            TableManager.Instance.HighlightObject += OnHighlightObject;
            TableManager.Instance.ClearHighlights += OnResetHighlightObject;
        }

        private void OnDestroy()
        {
            TableManager.Instance.HighlightObject -= OnHighlightObject;
            TableManager.Instance.ClearHighlights -= OnResetHighlightObject;
        }
        #endregion

        #region Event Listeners
        private void OnHighlightObject(string gameID)
        {
            
            this.StartHighlightTable();
        }

        private void OnResetHighlightObject(string gameID)
        {
            this.StartSetDefaultHighlightTable();
        }
        #endregion
        public void StartHighlightTable()
        {
            if (this.tableHiglightRoutine != null)
            {
                return;
            }

            this.tableHiglightRoutine = StartCoroutine(this.HighlightTableCoroutine());
        }

        public void StartSetDefaultHighlightTable()
        {
            if (this.defaultTableHiglightRoutine != null)
            {
                return;
            }

            this.defaultTableHiglightRoutine = StartCoroutine(this.SetDefaultHighlightTableCoroutine());
        }

        private IEnumerator HighlightTableCoroutine()
        {
            foreach (MeshRenderer renderer in tableMeshes)
            {
                renderer.material = highlightMat;
                yield return null;
            }
            this.tableHiglightRoutine = null;
        }

        private IEnumerator SetDefaultHighlightTableCoroutine()
        {
            foreach (MeshRenderer renderer in tableMeshes)
            {
                renderer.material = defaultMat;
                yield return null;
            }
            this.defaultTableHiglightRoutine = null;
        }
    }
}
