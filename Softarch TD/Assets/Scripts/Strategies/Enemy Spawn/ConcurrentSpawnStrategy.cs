using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A spawn strategy used by <see cref="EnemyGroup"/>s.
/// 
/// </summary>
[CreateAssetMenu(fileName = "ConcurrentSpawn", menuName = "Strategy/Spawn/Concurrent")]
public class ConcurrentSpawnStrategy : SpawnStrategyBase
{
    public override Queue<Tuple<EnemyScriptable, float>> GetSpawnOrder(EnemyGroup pGroup)
    {
        Queue<Tuple<EnemyScriptable, float>> spawnOrder = new Queue<Tuple<EnemyScriptable, float>>();

        List<Tuple<EnemyScriptable, float>> groupInfo = new List<Tuple<EnemyScriptable, float>>(pGroup.EnemyTypes.Count);

        List<int> spawnCount = new List<int>(pGroup.EnemyTypes.Count);
        List<float> delayBaseValues = new List<float>(pGroup.EnemyTypes.Count);
        List<float> delayCounters = new List<float>(pGroup.EnemyTypes.Count);

        for (int i = 0; i < pGroup.EnemyTypes.Count; i++)
        {
            groupInfo.Add(Tuple.Create(pGroup.EnemyTypes[i].EnemyType, pGroup.EnemyTypes[i].SpawnDelay));
        }

        groupInfo.Sort(CompareTupleSpawnDelays);

        for (int i = 0; i < groupInfo.Count; i++)
        {
            delayBaseValues.Add(groupInfo[i].Item2);
            delayCounters.Add(groupInfo[i].Item2);
            spawnCount.Add(pGroup.EnemyTypes[i].SpawnCount - 1);
        }


        for (int i = 0; i < groupInfo.Count-1; i++)
        {
            spawnOrder.Enqueue(Tuple.Create(groupInfo[i].Item1, 0f));
        }

        int smallestIndex = GetSmallestDelayIndex(delayCounters);

        spawnOrder.Enqueue(Tuple.Create(groupInfo.Last().Item1, delayCounters[smallestIndex]));

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

            spawnOrder.Enqueue(Tuple.Create(groupInfo[smallestIndex].Item1, delayCounters[smallestIndex]));

            spawnCount[smallestIndex]--;

            UpdateDelayCounters(smallestIndex, delayCounters, delayBaseValues);
        }

        return spawnOrder;
    }

    private int CompareTupleSpawnDelays(Tuple<EnemyScriptable, float> x, Tuple<EnemyScriptable, float> y)
    {
        if (Mathf.Approximately(x.Item2, y.Item2)) return 0;
        if (x.Item2 > y.Item2) return 1;
        return -1;
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
        float smallestDelay = pDelayCounters[pSmallestIndex];

        for (int i = 0; i < pDelayCounters.Count; i++)
        {
            pDelayCounters[i] -= smallestDelay;
        }

        pDelayCounters[pSmallestIndex] = pDelayBaseValues[pSmallestIndex];
    }
}
