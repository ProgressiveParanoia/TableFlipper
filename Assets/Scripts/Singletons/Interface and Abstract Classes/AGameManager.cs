using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGameManager : MonoBehaviour
{
    public abstract void Setup();
    protected abstract IEnumerator SetupRoutine();
}
