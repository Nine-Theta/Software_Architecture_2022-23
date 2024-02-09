using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EnemyFactory))]
public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyFactory _spawnFactory;

    [SerializeField]
    private Vector3 _spawnOffset = Vector3.zero;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private WaveScriptable[] _enemyWaves;
    
    private WaveScriptable _currentWave;

    private EnemyGroup _currentGroup;

    private int _groupCounter = 0; //which group is being handled right now

    private int _groupCount = 1; 

    private Queue<Tuple<EnemyScriptable, float>> _currentSpawnList;

    public bool IsWaveComplete { get; private set; } = false;

    public EventPublisher SpawnerWaveCompleted = new EventPublisher();

    private void Awake()
    {
        if(_spawnFactory == null)
        {
            _spawnFactory = gameObject.GetComponent<EnemyFactory>();
        }
    }

    [Button]
    public void TestSpawn()
    {
        SpawnWave(0);
    }

    public void SpawnWave(int pWaveIndex)
    {
        if (_enemyWaves[pWaveIndex] == null) return;

        _currentWave = _enemyWaves[pWaveIndex];

        _groupCounter = 0;
        _groupCount = _currentWave.EnemyGroups.Count;

        LoadNextGroup();
    }

    private void LoadNextGroup()
    {
        if(_groupCounter >= _groupCount)
        {
            IsWaveComplete = true;
            SpawnerWaveCompleted.Publish();
            return;
        }

        _currentGroup = _currentWave.EnemyGroups[_groupCounter];

        _currentSpawnList = _currentGroup.SpawnStrategy.GetSpawnOrder(_currentGroup);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (_currentSpawnList.Count > 0)
        {
            _spawnFactory.SetEnemyVariant(_currentSpawnList.Peek().Item1);

            EnemyObject enemy = _spawnFactory.CreateInstance(transform.position + _spawnOffset).GetComponent<EnemyObject>();
            enemy.TargetPos = _target.transform.position;
            enemy.Move();

            //Debug.Log("spawned: " + _currentSpawnList.Peek().Item1.GetName + " with delay of:" + _currentSpawnList.Peek().Item2);

            yield return new WaitForSeconds(_currentSpawnList.Dequeue().Item2);
        }

        Debug.Log("spawning done for now");
        _groupCounter++;

        yield return new WaitForSeconds(_currentGroup.GroupSpawnDelay);

        Debug.Log("Loading next group");

        LoadNextGroup();
    }

    public int GetTotalWaveCount()
    {
        return _enemyWaves.Count();
    }
}
