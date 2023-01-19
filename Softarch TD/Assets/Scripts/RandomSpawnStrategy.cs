using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnStrategy : SpawnStrategyBase
{
    private int _groupCount = 0;
    private int[] _enemyCounter;

    public override void SpawnGroup(EnemyGroupScriptable pGroup)
    {
        _groupCount = pGroup.EnemyGroup.Count;

        EnemyList = pGroup.EnemyGroup;

        _enemyCounter = new int[_groupCount];

        for (int i = 0; i < pGroup.EnemyGroup.Count; i++)
        {
            _enemyCounter[i] = pGroup.EnemyGroup[i].EnemyCount;
        }

        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        int type = Random.Range(0, _groupCount-1);

        SpawnEnemy(EnemyList[type].EnemyType);

        float delay = EnemyList[type].SpawnDelay;

        _enemyCounter[type]--;

        if (_enemyCounter[type] <= 0)
        {
            EnemyList.RemoveAt(type);
            _groupCount--;

            if (_groupCount <= 0) //Todo: Fire event when done.
                StopCoroutine(Spawner());
           
        }

        yield return new WaitForSeconds(delay);

    }
}
