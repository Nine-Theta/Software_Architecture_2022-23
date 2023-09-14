using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public TowerScriptable TestTower;

    [Button]
    public void TestSpawn()
    {
        CreateTower(TestTower);
    }

    //TODO: this and enemyfactory look a lot like each other, maybe they can be consolidated, like an abstract scriptable factory or something
    public GameObject CreateTower(TowerScriptable pTowerData)
    {
        GameObject newTower = new GameObject(pTowerData.Name); //Instantiate(,new GameObject(pEnemyData.Name), Vector3.zero, Quaternion.identity); //pPosition, pRotation);
        GameObject towerModel = Instantiate(pTowerData.TowerModel, Vector3.zero, Quaternion.identity, newTower.transform);

        TowerObject f = newTower.AddComponent<TowerObject>();
        CapsuleCollider g = newTower.AddComponent<CapsuleCollider>();
        g.radius = pTowerData.Range;
        g.isTrigger = true;
        
        f.Initialize(pTowerData, towerModel, g);

        return newTower; ;
    }
}
