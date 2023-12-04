using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSpawn", menuName = "Strategy/Spawn/Random")]
public class RandomSpawnStrategy : SpawnStrategyBase
{
    public override List<Tuple<EnemyScriptable, float>> GetSpawnOrder(EnemyGroup pGroup)
    {
        List<Tuple<EnemyScriptable, float>> spawnOrder = new List<Tuple<EnemyScriptable, float>>();

        for (int i = 0; i < pGroup.EnemyTypes.Count; i++)
        {
            for(int j = 0; j < pGroup.EnemyTypes[i].SpawnCount; j++)
            {
                spawnOrder.Add(Tuple.Create(pGroup.EnemyTypes[i].EnemyType, pGroup.EnemyTypes[i].SpawnDelay));
            }
        }

        Shuffle(spawnOrder);

        return spawnOrder;
    }

    public void Shuffle<T>(List<T> list)
    {
        int rand;
        T item;

        for (int i = list.Count; i > 1;)
        {
            rand = UnityEngine.Random.Range(0, i--);
            item = list[i];
            list[i] = list[rand];
            list[rand] = item;
        }
    }
}
