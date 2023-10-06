using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : AbstractInstanceFactory
{
    [SerializeField]
    private TowerScriptable _tower;

    [Button]
    public void TestSpawn()
    {
        CreateInstance(new Vector3(0,1,0));
    }

    public override AbstractContainerObject CreateInstance(Vector3 pPosition)
    {
        GameObject newTower = Instantiate(_tower.GetContainerObject, pPosition, Quaternion.identity);
        newTower.name = _tower.GetName;

        TowerObject instance = newTower.GetComponent<TowerObject>();

        instance.Initialize(_tower);

        return instance;
    }

    public override void SetContainable(I_Containable pTowerScriptable)
    {
        SetTowerVariant(pTowerScriptable as TowerScriptable);
    }

    public void SetTowerVariant(TowerScriptable pTowerScriptable)
    {
        _tower = pTowerScriptable;
    }
}
