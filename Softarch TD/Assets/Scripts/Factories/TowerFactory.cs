using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : AbstractScriptableInstanceFactory<TowerScriptable>
{
    public TowerScriptable TestTower;

    [Button]
    public void TestSpawn()
    {
        CreateInstance(TestTower);
    }

    //TODO: this and enemyfactory look a lot like each other, maybe they can be consolidated, like an abstract scriptable factory or something
    public override GameObject CreateInstance(TowerScriptable pTowerData)
    {
        GameObject newTower = Instantiate(pTowerData.GetContainerObject, Vector3.zero, Quaternion.identity);
        newTower.name = pTowerData.GetName;

        newTower.GetComponent<TowerObject>().Initialize(pTowerData);

        return newTower;
    }
}
