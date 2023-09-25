using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractScriptableInstanceFactory<T> : MonoBehaviour where T : I_Containable
{
    public abstract GameObject CreateInstance(T pObjectData, Vector3 pPosition);
}
