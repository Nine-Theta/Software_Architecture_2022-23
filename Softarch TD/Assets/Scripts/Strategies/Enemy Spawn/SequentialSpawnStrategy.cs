using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SequentialSpawn", menuName = "Strategy/Spawn/Sequential")]
public class SequentialSpawnStrategy : SpawnStrategyBase
{
    public override Queue<Tuple<EnemyScriptable, float>> GetSpawnOrder(EnemyGroup pGroup)
    {
        Queue<Tuple<EnemyScriptable, float>> spawnOrder = new Queue<Tuple<EnemyScriptable, float>>();

        for (int i = 0; i < pGroup.EnemyTypes.Count; i++)
        {
            for (int j = 0; j < pGroup.EnemyTypes[i].SpawnCount; j++)
            {
                spawnOrder.Enqueue(Tuple.Create(pGroup.EnemyTypes[i].EnemyType, pGroup.EnemyTypes[i].SpawnDelay));
            }
        }

        return spawnOrder;
    }
}
