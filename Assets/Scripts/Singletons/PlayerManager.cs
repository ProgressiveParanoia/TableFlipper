using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowMonsters.Utilities;
using System;

public class PlayerManager : AGameManager {

    #region Singleton implementation
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get { return instance; }
    }
    #endregion

    #region AGameManager implementation
    public override void Setup()
    {
        MonoUtility.Instance.StartCoroutine(SetupRoutine());
    }

    protected override IEnumerator SetupRoutine()
    {
        Vector3 playerSpawnPosition = GameObject.Find("PlayerSpawn").transform.position;
        GameObject p = GameObject.Instantiate(Resources.Load(IngameFileList.INGAME_PLAYER_PREFAB_PLACEHOLDER_PATH)) as GameObject;
        p.transform.position = playerSpawnPosition;
        yield return null;

    }

    #endregion

    
    #region Mono
    private void Awake()
    {
        instance = this.GetComponent<PlayerManager>();
    }
#endregion
}
