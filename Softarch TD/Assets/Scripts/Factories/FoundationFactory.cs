using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationFactory : AbstractScriptableInstanceFactory<FoundationScriptable>
{
    public GameObject FoundationTemplate;

    public float SnapSize;

    public override GameObject CreateInstance(FoundationScriptable pFoundationData, Vector3 pPosition)
    {

        Vector3 pos = new Vector3(pPosition.x - (pPosition.x % SnapSize), pPosition.y, pPosition.z - (pPosition.z % SnapSize));

        GameObject founder = Instantiate(pFoundationData.GetContainerObject, pos, Quaternion.identity);

        founder.name = pFoundationData.GetName;

        founder.GetComponent<FoundationObject>().Initialize(pFoundationData);


        return founder;
    }
}
