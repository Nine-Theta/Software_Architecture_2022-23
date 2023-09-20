using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractContainerObject<T> : MonoBehaviour where T : I_Containable
{
    public abstract T BaseData { get; }

    public abstract void Initialize(T pData);

}
