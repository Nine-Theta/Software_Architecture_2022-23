using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

public abstract class SpawnStrategyBase : MonoBehaviour
{
    public GameObject EnemyTemplate;
    public EnemyScriptable testEnemy;

    protected List<EnemySpawnSettings> EnemyList;

    private void Start()
    {
        SpawnEnemy(testEnemy);
    }

    public void SpawnEnemy(EnemyScriptable pEnemyData)
    {
        GameObject newEnemy = Instantiate(EnemyTemplate);
        newEnemy.GetComponent<EnemyShellScript>().InitEnemy(pEnemyData);
    }

    public abstract void SpawnGroup(EnemyGroupScriptable pGroup);
}
