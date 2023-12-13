using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private int _currentWaveCount = 0;
    private int _wavesInLevel = 1;

    [SerializeField]
    private EnemyWaveSpawner[] _spawnLocations;

    public int TotalWaves { get { return _wavesInLevel; } }

    public EventPublisher<int, int> SpawningWave = new EventPublisher<int, int>();
    public EventPublisher CurrentWaveComplete = new EventPublisher();
    public EventPublisher AllWavesComplete = new EventPublisher();

    private void Start()
    {
        for(int i = 0; i < _spawnLocations.Length; i++)
        {
            _spawnLocations[i].SpawnWaveComplete.Subscribe(CheckIfSpawningFinished);
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

        CurrentWaveComplete.Publish();
    }

    public void SpawnNextWave()
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

        SpawningWave.Publish(_currentWaveCount, _wavesInLevel);
    }

}
