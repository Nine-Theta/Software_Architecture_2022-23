using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractInstanceFactory : MonoBehaviour
{
    public abstract AbstractContainerObject CreateInstance(Vector3 pPosition);

    public abstract void SetContainable(I_Containable pContainable);

    public virtual void DeleteInstance(AbstractContainerObject pInstance)
    {
        Destroy(pInstance.gameObject);
    }

    public virtual LayerMask GetBuildLayer()
    {
        return LayerMask.NameToLayer("Default");
    }
}
