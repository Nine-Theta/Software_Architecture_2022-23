using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationObject : AbstractContainerObject<FoundationScriptable>
{
    public FoundationScriptable _baseData;

    public override FoundationScriptable BaseData { get { return _baseData; } }

    public override void Initialize(FoundationScriptable pData)
    {

    }
}
