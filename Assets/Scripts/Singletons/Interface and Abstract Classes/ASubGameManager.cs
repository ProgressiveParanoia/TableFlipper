using System.Collections;
using UnityEngine;
public abstract class ASubGameManager
{
    public abstract void Setup();
    protected abstract IEnumerator SetupRoutine();
}
