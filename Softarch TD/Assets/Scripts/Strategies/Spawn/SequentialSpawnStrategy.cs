using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SequentialSpawn", menuName = "SpawnStrategy/Sequential")]
public class SequentialSpawnStrategy : SpawnStrategyBase
{
    private List<int> _enemyCounter;

    public override event System.Action OnSpawningComplete;

    int _typeIndex;

    public override void SpawnGroup(EnemyGroupScriptable pGroup, MonoBehaviour pMono)
    {        
        EnemyList = new List<EnemySpawnSettings>(pGroup.EnemyTypes);

        _enemyCounter = new List<int>(pGroup.EnemyTypes.Count);

        _typeIndex = 0;

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

        SpawnEnemy(EnemyList[_typeIndex].EnemyType);

        float delay = EnemyList[_typeIndex].SpawnDelay;

        _enemyCounter[_typeIndex]--;

        yield return new WaitForSeconds(delay);

        Debug.Log("type " + _typeIndex);
        Debug.Log("list count "+EnemyList.Count);
        Debug.Log("type name: "+ EnemyList[_typeIndex].EnemyType.name);


        if (_enemyCounter[_typeIndex] <= 0)
        {
            Debug.Log("No enemies left of type");
            _typeIndex++;
        }

        if (_typeIndex >= _enemyCounter.Count)
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
