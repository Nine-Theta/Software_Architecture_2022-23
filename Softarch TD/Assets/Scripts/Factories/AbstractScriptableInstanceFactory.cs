using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FactoryType { Tower, Foundation, Enemy }
public abstract class AbstractInstanceFactory : MonoBehaviour
{
    public abstract AbstractContainerObject CreateInstance(Vector3 pPosition);

    public abstract void SetContainable(I_Containable pContainable);

    public virtual void DeleteInstance(AbstractContainerObject pInstance)
    {
        Destroy(pInstance.gameObject);
    }    
}
