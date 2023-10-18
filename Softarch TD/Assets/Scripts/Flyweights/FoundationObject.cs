using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationObject : AbstractContainerObject
{
    [SerializeField]
    private FoundationScriptable _baseData;

    private bool _buildable = true;
    public bool Buildable { get { return _buildable; } }

    public Vector3 GetBuildPos { get { return transform.position + new Vector3(0,transform.localScale.y*0.5f,0); } }

    public override I_Containable BaseData { get { return _baseData; } }

    public override void Initialize(I_Containable pData)
    {

    }

    public bool BuildRequest()
    {
        if (_buildable)
        {
            _buildable = false;
            return true;
        }
        else
            return false;
    }
}
