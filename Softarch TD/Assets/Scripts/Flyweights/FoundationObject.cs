using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationObject : AbstractContainerObject
{
    [SerializeField]
    private FoundationScriptable _baseData;

    private bool _buildable = true;
    public bool Buildable { get { return _buildable; } set { _buildable = value; } }

    public override I_Containable BaseData { get { return _baseData; } }

    public override void Initialize(I_Containable pData)
    {

    }

}
