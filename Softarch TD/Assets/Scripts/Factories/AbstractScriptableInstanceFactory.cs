using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractInstanceFactory : MonoBehaviour
{
    public abstract I_Containable Containable { get; set; }

    public abstract AbstractContainerObject CreateInstance(Vector3 pPosition);

    public virtual void DeleteInstance(AbstractContainerObject pInstance)
    {
        Destroy(pInstance.gameObject);
    }

    public virtual LayerMask GetBuildLayer()
    {
        return LayerMask.NameToLayer("Default");
    }
}
