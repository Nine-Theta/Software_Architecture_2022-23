using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationFactory : AbstractInstanceFactory
{
    [SerializeField]
    private FoundationScriptable _foundation;

    [SerializeField]
    private FoundationObject _foundationObject;

    [SerializeField]
    private LayerMask _buildLayer;

    public float SnapSize;

    public FoundationScriptable Foundation
    {
        get { return _foundation; }
        set { _foundation = value; }
    }

    public override I_Containable Containable
    {
        get { return _foundation; }
        set { _foundation = value is FoundationFactory ? value as FoundationScriptable : throw new System.ArgumentException("Incorrect Containable", "Foundation Factory"); }
    }
    public AbstractContainerObject CreateInstance(Vector3 pPosition)
    {
        return CreateInstance(pPosition, Quaternion.identity);
    }

    public override AbstractContainerObject CreateInstance(Vector3 pPosition, Quaternion pRotation)
    {
        float x = pPosition.x % SnapSize;
        float z = pPosition.z % SnapSize;

        Vector3 pos = new Vector3((pPosition.x - x) + Mathf.Round(x), pPosition.y, (pPosition.z - z) + Mathf.Round(z));

        GameObject founder = Instantiate(_foundationObject.gameObject, pos, pRotation);
        GameObject model = Instantiate(_foundation.GetModel, _foundation.GetModel.transform.position + pos, pRotation, founder.transform);        

        founder.name = _foundation.GetName;

        FoundationObject instance = founder.GetComponent<FoundationObject>();

        instance.Initialize(_foundation, model);

        return instance;
    }

    public override LayerMask GetBuildLayer()
    {
        return _buildLayer;
    }
}
