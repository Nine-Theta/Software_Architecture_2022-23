using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationFactory : AbstractInstanceFactory
{
    [SerializeField]
    private FoundationScriptable _foundation;

    [SerializeField]
    private LayerMask _buildLayer;

    public float SnapSize;

    public override AbstractContainerObject CreateInstance(Vector3 pPosition)
    {

        Vector3 pos = new Vector3(pPosition.x - (pPosition.x % SnapSize), pPosition.y, pPosition.z - (pPosition.z % SnapSize));

        GameObject founder = Instantiate(_foundation.GetContainerObject, pos, Quaternion.identity);

        founder.name = _foundation.GetName;

        FoundationObject instance = founder.GetComponent<FoundationObject>();

        instance.Initialize(_foundation);

        return instance;
    }
    
    public override void SetContainable(I_Containable pFoundationScriptable)
    {
        SetFoundation(pFoundationScriptable as FoundationScriptable);
    }

    public void SetFoundation(FoundationScriptable pFoundation)
    {
        _foundation = pFoundation;
    }

    public override LayerMask GetBuildLayer()
    {
        return _buildLayer;
    }
}
