using System;
using System.Collections;
using System.Collections.Generic;
using ShadowMonsters.Utilities;
using UnityEngine;


public class GameManager : AGameManager {

    #region fields and properties
    [SerializeField]
    private List<AGameManager> ingameMangers;
    #endregion

    #region implemented AGameManager methods
    public override void Setup()
    {
        MonoUtility.Instance.StartCoroutine(SetupRoutine());
		Debug.LogError("Push me baby pls");
	}

    protected override IEnumerator SetupRoutine()
    {
        foreach (AGameManager manager in ingameMangers)
        {
            GameObject gameManager = GameObject.Instantiate(manager.gameObject);
            yield return gameManager;
        }
    }
    #endregion

    #region Mono
    private void Awake()
    {
        //TODO: setup via awake for now until we get proper UI
        this.Setup();
    }
    #endregion
}
