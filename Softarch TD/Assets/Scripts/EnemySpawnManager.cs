using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all the <see cref="EnemyWaveSpawner"/>s in a scene.
/// </summary>
public class EnemySpawnManager : MonoBehaviour
{
    private int _currentWaveCount = 0;
    private int _wavesInLevel = 1;

    [SerializeField]
    private EnemyWaveSpawner[] _spawnLocations;

    public int CurrentWaveCount { get { return _currentWaveCount; } }
    public int TotalWaves { get { return _wavesInLevel; } }

    public EventPublisher<int> CurrentWaveComplete = new EventPublisher<int>();
    public EventPublisher AllWavesComplete = new EventPublisher();

    private void Start()
    {
        for(int i = 0; i < _spawnLocations.Length; i++)
        {
            _spawnLocations[i].SpawnerWaveCompleted.Subscribe(CheckIfSpawningFinished);
            if(_wavesInLevel < _spawnLocations[i].GetTotalWaveCount())
                _wavesInLevel = _spawnLocations[i].GetTotalWaveCount();
        }
    }

    private void CheckIfSpawningFinished()
    {
        for (int i = 0; i < _spawnLocations.Length; i++)
        {
            if (!_spawnLocations[i].IsWaveComplete)
                return;
        }

        CurrentWaveComplete.Publish(_currentWaveCount);
    }

    public void SpawnNextWave() //TODO: ensure waves are done spawning
    {
        if(_currentWaveCount >= _wavesInLevel)
        {
            AllWavesComplete.Publish();
            return;
        }

        for (int i = 0; i < _spawnLocations.Length; i++)
        {
            _spawnLocations[i].SpawnWave(_currentWaveCount);
        }
        _currentWaveCount++;
    }

}
