using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowMonsters.Utilities
{
    public class MonoUtility : MonoBehaviour
    {
        private static MonoUtility instance;

        private void Awake()
        {
            instance = this;
        }

        #region singleton impelmentation
        public static MonoUtility Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject g = new GameObject("MonoUtility");
                    g.AddComponent<MonoUtility>();
                    instance = g.GetComponent<MonoUtility>();
                }
                return instance;
            }
        }
        #endregion
    }
}
