using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFactory : AbstractScriptableInstanceFactory<EnemyScriptable>
{
    public EnemyScriptable testEnemy;

    [Button]
    public void TestSpawn()
    {
        CreateInstance(testEnemy);
    }

    public override GameObject CreateInstance(EnemyScriptable pEnemyData)
    {
        GameObject newEnemy = Instantiate(pEnemyData.GetContainerObject, Vector3.zero, Quaternion.identity); //pPosition, pRotation);
        newEnemy.name = pEnemyData.GetName;

        newEnemy.GetComponent<EnemyObject>().Initialize(pEnemyData);

        return newEnemy; ;
    }
}
