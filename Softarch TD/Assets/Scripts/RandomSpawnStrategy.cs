using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSpawn", menuName = "SpawnStrategy/Random")]
public class RandomSpawnStrategy : SpawnStrategyBase
{
    private int _groupCount = 0;
    private int[] _enemyCounter;

    public override event System.Action OnSpawningComplete;

    public override void SpawnGroup(EnemyGroupScriptable pGroup, MonoBehaviour pMono)
    {
        _groupCount = pGroup.EnemyTypes.Count;

        EnemyList = new List<EnemySpawnSettings>(pGroup.EnemyTypes);

        _enemyCounter = new int[_groupCount];
        Debug.Log("groupcount: " + _groupCount);

        for (int i = 0; i < pGroup.EnemyTypes.Count; i++)
        {
            _enemyCounter[i] = pGroup.EnemyTypes[i].EnemyCount;

            Debug.Log("nmecount: " + _enemyCounter[i]);
        }

        mono = pMono;
        mono.StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {

        Debug.Log("routine started");
        int type = Random.Range(0, _groupCount - 1);

        SpawnEnemy(EnemyList[type].EnemyType);

        float delay = EnemyList[type].SpawnDelay;

        _enemyCounter[type]--;


        yield return new WaitForSeconds(delay);

        if (_enemyCounter[type] <= 0)
        {
            EnemyList.RemoveAt(type);
            _groupCount--;
        }

        if (_groupCount <= 0) //Todo: Fire event when done.
        {
            Debug.Log("No enemies left of type");
            OnSpawningComplete?.Invoke();
            mono.StopCoroutine(Spawner());
        }
        else
        {
            mono.StartCoroutine(Spawner());
        }
    }
}
