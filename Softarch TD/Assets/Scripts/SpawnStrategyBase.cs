using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

public abstract class SpawnStrategyBase : ScriptableObject
{
    public GameObject EnemyTemplate;

    public abstract event System.Action OnSpawningComplete;

    protected MonoBehaviour mono;

    protected List<EnemySpawnSettings> EnemyList;

    public void SpawnEnemy(EnemyScriptable pEnemyData)
    {
        GameObject newEnemy = Instantiate(EnemyTemplate);
        newEnemy.GetComponent<EnemyShellScript>().Initialize(pEnemyData);
    }


    public abstract void SpawnGroup(EnemyGroupScriptable pGroup, MonoBehaviour pMono);
}
