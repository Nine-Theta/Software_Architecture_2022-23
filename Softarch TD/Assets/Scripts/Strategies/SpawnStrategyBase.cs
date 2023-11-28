using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

public abstract class SpawnStrategyBase/*<T>*/ : ScriptableObject
{
    public abstract event System.Action OnSpawningComplete;

    public abstract event System.Action/*<T>*/ OnNextSpawn;

    protected MonoBehaviour mono;

    protected List<EnemySpawnSettings/*<T>*/> Spawnables;

    public abstract void SpawnGroup(List<EnemySpawnSettings/*<T>*/> pSpawnables, MonoBehaviour pMono);
}
