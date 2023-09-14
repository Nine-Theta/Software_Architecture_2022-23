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
        GameObject newEnemy = new GameObject(pEnemyData.Name); //Instantiate(,new GameObject(pEnemyData.Name), Vector3.zero, Quaternion.identity); //pPosition, pRotation);
        GameObject enemyModel = Instantiate(pEnemyData.EnemyModel, Vector3.zero, Quaternion.identity,newEnemy.transform);

        EnemyObject f = newEnemy.AddComponent<EnemyObject>();
        f.Initialize(pEnemyData, enemyModel);

        return newEnemy; ;
    }
}
