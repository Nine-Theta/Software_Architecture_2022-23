using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : AbstractInstanceFactory
{
    [SerializeField]
    private TowerScriptable _tower;

    [SerializeField]
    private TowerObject _towerObject;

    [SerializeField]
    private LayerMask _buildLayer;

    public TowerScriptable TowerVariant
    {
        get { return _tower; }
        set { _tower = value; }
    }

    public override I_Containable Containable
    {
        get { return _tower; }
        set { _tower = value is TowerScriptable ? value as TowerScriptable : throw new System.ArgumentException("Incorrect Containable", "TowerFactory"); }
    }

    [Button]
    public void TestBuildTower()
    {
        CreateInstance(transform.position);
    }
    public AbstractContainerObject CreateInstance(Vector3 pPosition)
    {
        return CreateInstance(pPosition, Quaternion.identity);
    }

    public override AbstractContainerObject CreateInstance(Vector3 pPosition, Quaternion pRotation)
    {      
        GameObject newTower = Instantiate(_towerObject.gameObject, pPosition, pRotation);
        //pPosition.y += _tower.ModelHeightOffset;
        GameObject model = Instantiate(_tower.GetModel, _tower.GetModel.transform.position + pPosition, pRotation, newTower.transform);
        newTower.name = _tower.GetName;

        TowerObject instance = newTower.GetComponent<TowerObject>();

        instance.Initialize(_tower, model);

        return instance;
    }

    public override LayerMask GetBuildLayer()
    {
        return _buildLayer;
    }
}
