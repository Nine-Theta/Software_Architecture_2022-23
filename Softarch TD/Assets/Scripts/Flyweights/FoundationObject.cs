using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles all functionalities of a foundation, it contains an <see cref="FoundationScriptable"/> that has the instantiation values.
/// </summary>
/// <remarks>It is instantiated by an <see cref="FoundationFactory"/></remarks>
[SelectionBase]
public class FoundationObject : AbstractContainerObject
{
    [SerializeField]
    private FoundationScriptable _baseData;

    private GameObject _model; 

    private bool _buildable = true;
    public bool Buildable { get { return _buildable; } }

    public Vector3 GetBuildPos { get { return transform.position; } }

    public override I_Containable BaseData { get { return _baseData; } }

    public override void Initialize(I_Containable pData, GameObject pFoundationModel)
    {
        _baseData = pData as FoundationScriptable;

        _model = pFoundationModel;
    }
    public GameObject GetModel() { return _model; }

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

    public void ClearBuildStatus()
    {
        _buildable = true;
    }
}
