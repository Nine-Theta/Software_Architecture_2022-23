//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "SequentialSpawn", menuName = "SpawnStrategy/Sequential")]
//public class SequentialSpawnStrategy : SpawnStrategyBase
//{
//    private List<int> _enemyCounter;

//    public override event System.Action OnSpawningComplete;

//    int _typeIndex;

//    int _enemyTotal;

//    public override void SpawnGroup(EnemyGroupScriptable pGroup, MonoBehaviour pMono)
//    {
//        //EnemyList = new List<EnemySpawnSettings>(pGroup.EnemyTypes);

//        _enemyCounter = new List<int>(pGroup.EnemyTypes.Count);

//        _typeIndex = 0;
//        _enemyTotal = 0;

//        Debug.Log("groupcount: " + pGroup.EnemyTypes.Count);

//        for (int i = 0; i < pGroup.EnemyTypes.Count; i++) //For each enemy type in the group
//        {
//            //_enemyCounter.Add(pGroup.EnemyTypes[i].EnemyCount); //how many enemies per type
//            _enemyTotal += _enemyCounter[i];
//        }

//        Debug.Log("total: " + _enemyTotal);

//        //mono = pMono;
//        //mono.StartCoroutine(Spawner());
//    }

//    IEnumerator Spawner()
//    {
//        Debug.Log("routine started");
//        Debug.Log("total: " + _enemyTotal);
//        /*
//        if (_typeIndex >= EnemyList.Count)
//        {
//            _typeIndex = 0;
//        }
//        */
//        if (_enemyCounter[_typeIndex] <= 0)
//        {
//            Debug.Log("No enemies left of type");
//            _typeIndex++;
//            //mono.StartCoroutine(Spawner());
//            yield break;
//        }

//        //SpawnEnemy(EnemyList[_typeIndex].EnemyType);

//        //float delay = EnemyList[_typeIndex].SpawnDelay;

//        _enemyCounter[_typeIndex]--;

//        _enemyTotal--;

//        Debug.Log("type " + _typeIndex);
//        //Debug.Log("list count " + EnemyList.Count);
//        //Debug.Log("nme remaining of type " + _enemyCounter[_typeIndex]);
//        //Debug.Log("type name: " + EnemyList[_typeIndex].EnemyType.name);

//        _typeIndex++;

//        //yield return new WaitForSeconds(delay);

//        Debug.Log("total: " + _enemyTotal);

//        if (_enemyTotal <= 0)
//        {
//            Debug.Log("No enemies left of any type");
//            OnSpawningComplete?.Invoke();
//            yield break;
//        }

//        //mono.StartCoroutine(Spawner());
//    }
//}
