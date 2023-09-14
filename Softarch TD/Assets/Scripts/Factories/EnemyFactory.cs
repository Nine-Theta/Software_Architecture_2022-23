using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFactory : MonoBehaviour
{
    public EnemyScriptable testEnemy;

    [Button]
    public void TestSpawn()
    {
        CreateEnemy(testEnemy);
    }

    public GameObject CreateEnemy(EnemyScriptable pEnemyData)
    {
        GameObject newEnemy = Instantiate(pEnemyData.EnemyModel, Vector3.zero, Quaternion.identity); //pPosition, pRotation);
        newEnemy.name = pEnemyData.Name;
        //GameObject enemyModel = Instantiate(pEnemyData.EnemyModel, Vector3.zero, Quaternion.identity,newEnemy.transform);

        EnemyObject f = newEnemy.GetComponent<EnemyObject>();

        f.Initialize(pEnemyData);

        return newEnemy; ;
    }
}
