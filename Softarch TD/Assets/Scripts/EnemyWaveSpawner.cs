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

    private Queue<Tuple<EnemyScriptable, float>> _currentSpawnList;

    public bool IsWaveComplete { get; private set; } = false;

    public event System.Action OnWaveComplete;

    public EventPublisher SpawnWaveComplete = new EventPublisher();

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
        _currentGroup = _currentWave.EnemyGroups[0];

        HandleCurrentGroup();
    }

    private void HandleCurrentGroup()
    {
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

            Debug.Log("spawned: " + _currentSpawnList.Peek().Item1.GetName + " with delay of:" + _currentSpawnList.Peek().Item2);

            yield return new WaitForSeconds(_currentSpawnList.Dequeue().Item2);
        }

        Debug.Log("spawning done for now");

        //TODO: next group;
    }

    public int GetTotalWaveCount()
    {
        return _enemyWaves.Count();
    }



    public void LoadWave(WaveScriptable pNewWave, bool pStartWave = true)
    {
        _currentWave = pNewWave;

        if (pStartWave)
        {
            HandleGroup(_currentWave.EnemyGroups.First());
            _groupCounter = 1;
        }
    }

    public void StartWave()
    {
        if (_currentWave == null) throw new System.Exception("Tried starting a wave, but no wave Loaded");

        HandleGroup(_currentWave.EnemyGroups.First());
        _groupCounter = 1;
    }

    public void OverideCooldown()
    {
        //Todo
    }

    private void HandleGroup(EnemyGroup pGroup)
    {
        _currentGroup = pGroup;

        //EnemySpawner.SpawnEnemyGroup(pGroup, Vector3.zero, Quaternion.identity);

        //_currentGroup.SpawnStrategy.SpawnGroup(pGroup, this);
        //_currentGroup.SpawnStrategy.OnSpawningComplete += ProgressWave;
    }

    private void ProgressWave()
    {
        Debug.Log("Wave Progress");

        //_currentGroup.SpawnStrategy.OnSpawningComplete -= ProgressWave;

        _groupCounter++;

        if(_groupCounter <= _currentWave.EnemyGroups.Count)
            StartCoroutine(GroupCooldown(_currentGroup.GroupSpawnDelay));
        else
            OnWaveComplete.Invoke();

    }

    private void OnDisable()
    {
        //_currentGroup.SpawnStrategy.OnSpawningComplete -= ProgressWave;
    }

    private IEnumerator GroupCooldown(float pSeconds)
    {
        yield return new WaitForSeconds(pSeconds);
        Debug.Log("In Routine");

        HandleGroup(_currentWave.EnemyGroups[_groupCounter - 1]);
    }

    private void Update()
    {
        
    }
}
