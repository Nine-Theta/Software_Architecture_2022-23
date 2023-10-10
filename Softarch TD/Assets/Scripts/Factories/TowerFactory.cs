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
    public void TestSpawn()
    {
        CreateInstance(new Vector3(0, 1, 0));
    }

    public override AbstractContainerObject CreateInstance(Vector3 pPosition)
    {
        GameObject newTower = Instantiate(_tower.GetContainerObject, pPosition, Quaternion.identity);
        newTower.name = _tower.GetName;

        TowerObject instance = newTower.GetComponent<TowerObject>();

        instance.Initialize(_tower);

        return instance;
    }

    public override LayerMask GetBuildLayer()
    {
        return _buildLayer;
    }
}
