using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ConcurrentSpawn", menuName = "Strategy/Spawn/Concurrent")]
public class ConcurrentSpawnStrategy : SpawnStrategyBase
{
    public override List<Tuple<EnemyScriptable, float>> GetSpawnOrder(EnemyGroup pGroup)
    {
        List<Tuple<EnemyScriptable, float>> spawnOrder = new List<Tuple<EnemyScriptable, float>>();

        List<Tuple<EnemyScriptable, float>> groupInfo = new List<Tuple<EnemyScriptable, float>>();

        List<int> spawnCount = new List<int>();
        List<float> delayBaseValues = new List<float>();
        List<float> delayCounters = new List<float>();

        for (int i = 0; i < pGroup.EnemyTypes.Count; i++)
        {
            //spawnOrder.Add(Tuple.Create(pGroup.EnemyTypes[i].EnemyType, pGroup.EnemyTypes[i].SpawnDelay));
            groupInfo.Add(Tuple.Create(pGroup.EnemyTypes[i].EnemyType, pGroup.EnemyTypes[i].SpawnDelay));
        }

        groupInfo.Sort(CompareTupleSpawnDelays);


        for (int i = 0; i < groupInfo.Count; i++)
        {
            delayBaseValues[i] = groupInfo[i].Item2;
            delayCounters[i] = groupInfo[i].Item2;
            spawnCount[i] = pGroup.EnemyTypes[i].SpawnCount - 1;
        }


        for (int i = 0; i < groupInfo.Count-1; i++)
        {
            spawnOrder.Add(Tuple.Create(groupInfo[i].Item1, 0f));
        }

        int smallestIndex = GetSmallestDelayIndex(delayCounters);

        spawnOrder.Add(Tuple.Create(groupInfo.Last().Item1, delayCounters[smallestIndex]));

        UpdateDelayCounters(smallestIndex, delayCounters, delayBaseValues);

        while (groupInfo.Count > 0)
        {
            smallestIndex = GetSmallestDelayIndex(delayCounters);

            if (spawnCount[smallestIndex] <= 0)
            {
                groupInfo.RemoveAt(smallestIndex);
                spawnCount.RemoveAt(smallestIndex);
                delayCounters.RemoveAt(smallestIndex);
                delayBaseValues.RemoveAt(smallestIndex);
                continue;
            }

            spawnOrder.Add(Tuple.Create(groupInfo[smallestIndex].Item1, groupInfo[smallestIndex].Item2));

            UpdateDelayCounters(smallestIndex, delayCounters, delayBaseValues);
        }

        return spawnOrder;
    }

    private int CompareTupleSpawnDelays(Tuple<EnemyScriptable, float> x, Tuple<EnemyScriptable, float> y)
    {
        if (x.Item2 == y.Item2) return 0;
        if (x.Item2 > y.Item2) return -1;
        return 1;
    }

    private int GetSmallestDelayIndex(List<float> pDelayCounters)
    {
        int index = 0;

        for (int i = 1; i < pDelayCounters.Count; i++)
        {
            if (pDelayCounters[index] > pDelayCounters[i])
                index = i;
        }

        return index;
    }

    private void UpdateDelayCounters(int pSmallestIndex, List<float> pDelayCounters, List<float> pDelayBaseValues)
    {
        for (int i = 0; i < pDelayCounters.Count; i++)
        {
            pDelayCounters[i] -= pDelayCounters[pSmallestIndex];
        }

        pDelayCounters[pSmallestIndex] = pDelayBaseValues[pSmallestIndex];
    }
}
