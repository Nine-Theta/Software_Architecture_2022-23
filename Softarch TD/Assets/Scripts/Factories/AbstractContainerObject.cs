using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractContainerObject : MonoBehaviour
{
    public abstract I_Containable BaseData { get; }

    public abstract void Initialize(I_Containable pData);

}
