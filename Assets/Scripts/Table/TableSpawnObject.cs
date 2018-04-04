using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowMonsters.Tables
{
    public class TableSpawnObject : MonoBehaviour
    {
        [SerializeField]
        private GameObject tableObject;

        #region fields and properties
        public GameObject GetTable
        {
            get { return tableObject; }
        }
        public Vector3 GetWorldPosition
        {
            get { return transform.position; }
        }

        public Vector3 GetLocalPosition
        {
            get { return transform.localPosition; }
        }

        #endregion

        #region Mono
        private void OnDestroy()
        {
            TableManager.Instance.TableSpawnManager.HideSpawnPoints -= OnHideSpawnPoint;
            TableManager.Instance.TableSpawnManager.ShowSpawnPoints -= OnShowSpawnPoint;
        }
        #endregion

        #region Setup
        public void Setup()
        {
            TableManager.Instance.TableSpawnManager.HideSpawnPoints += OnHideSpawnPoint;
            TableManager.Instance.TableSpawnManager.ShowSpawnPoints += OnShowSpawnPoint;
        }
        #endregion

        #region Event Listeners
        private void OnHideSpawnPoint()
        {
            this.tableObject.SetActive(false);
        }

        private void OnShowSpawnPoint()
        {
            this.tableObject.SetActive(true);
        }
        #endregion
    }
}
