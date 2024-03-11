using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The abstract class that is used as the base for objects that should be instantiated through the use of an <see cref="AbstractInstanceFactory"/> variant.
/// </summary>
/// <remarks>Used in conjuction with <see cref="I_Containable"/></remarks>
public abstract class AbstractContainerObject : MonoBehaviour
{
    public abstract I_Containable BaseData { get; }

    public abstract void Initialize(I_Containable pData, GameObject pObjectModel);

}
