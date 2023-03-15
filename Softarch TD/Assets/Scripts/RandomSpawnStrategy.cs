using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSpawn", menuName = "SpawnStrategy/Random")]
public class RandomSpawnStrategy : SpawnStrategyBase
{
    private List<int> _enemyCounter;

    public override event System.Action OnSpawningComplete;

    public override void SpawnGroup(EnemyGroupScriptable pGroup, MonoBehaviour pMono)
    {        
        EnemyList = new List<EnemySpawnSettings>(pGroup.EnemyTypes);

        _enemyCounter = new List<int>(pGroup.EnemyTypes.Count);

        Debug.Log("groupcount: " + pGroup.EnemyTypes.Count);

        for (int i = 0; i < pGroup.EnemyTypes.Count; i++) //For each enemy type in the group
        {
            _enemyCounter.Add(pGroup.EnemyTypes[i].EnemyCount); //how many enemies per type

            Debug.Log("nmecount: " + _enemyCounter[i]);
        }

        mono = pMono;
        mono.StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {

        Debug.Log("routine started");
        int type = Random.Range(0, _enemyCounter.Count);

        SpawnEnemy(EnemyList[type].EnemyType);

        float delay = EnemyList[type].SpawnDelay;

        _enemyCounter[type]--;


        yield return new WaitForSeconds(delay);

        Debug.Log("type " + type);
        Debug.Log("list count "+EnemyList.Count);
        Debug.Log("type name: "+ EnemyList[type].EnemyType.name);

        if (_enemyCounter[type] <= 0)
        {
            Debug.Log("No enemies left of type");
            EnemyList.RemoveAt(type);
            _enemyCounter.RemoveAt(type);
        }

        if (_enemyCounter.Count <= 0) //Todo: Fire event when done.
        {

            Debug.Log("No enemies left of any type");
            OnSpawningComplete?.Invoke();
            mono.StopCoroutine(Spawner());
        }
        else
        {
            mono.StartCoroutine(Spawner());
        }
    }
}
