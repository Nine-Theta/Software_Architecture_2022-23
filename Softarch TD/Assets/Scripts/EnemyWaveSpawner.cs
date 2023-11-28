using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField]
    private WaveScriptable[] _enemyWaves;
    
    private WaveScriptable _currentWave;

    private EnemyGroup _currentGroup;

    private int _groupCounter = 0; //which group is being handled right now

    public bool IsWaveComplete { get; private set; } = false;

    public event System.Action OnWaveComplete;

    public EventPublisher SpawnWaveComplete = new EventPublisher();

    private void Start()
    {
        StartWave();
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
        _currentGroup.SpawnStrategy.OnSpawningComplete += ProgressWave;
    }

    private void ProgressWave()
    {
        Debug.Log("Wave Progress");

        _currentGroup.SpawnStrategy.OnSpawningComplete -= ProgressWave;

        _groupCounter++;

        if(_groupCounter <= _currentWave.EnemyGroups.Count)
            StartCoroutine(GroupCooldown(_currentGroup.GroupSpawnDelay));
        else
            OnWaveComplete.Invoke();

    }

    private void OnDisable()
    {
        _currentGroup.SpawnStrategy.OnSpawningComplete -= ProgressWave;
    }

    private IEnumerator GroupCooldown(float pSeconds)
    {
        yield return new WaitForSeconds(pSeconds);
        Debug.Log("In Routine");

        HandleGroup(_currentWave.EnemyGroups[_groupCounter - 1]);
    }
}
