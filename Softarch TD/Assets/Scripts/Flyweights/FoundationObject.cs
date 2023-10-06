using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationObject : AbstractContainerObject
{
    [SerializeField]
    private FoundationScriptable _baseData;

    public override I_Containable BaseData { get { return _baseData; } }

    public override void Initialize(I_Containable pData)
    {

    }
}
