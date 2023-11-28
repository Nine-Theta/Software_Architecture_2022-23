using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private int _currentWaveCount = 0;

    [SerializeField]
    private EnemyWaveSpawner[] _spawnLocations;

    public EventPublisher CurrentWaveComplete = new EventPublisher();

    private void Start()
    {
        for(int i = 0; i < _spawnLocations.Length; i++)
        {
            _spawnLocations[i].SpawnWaveComplete.Subscribe(CheckIfSpawningFinished);
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
        for (int i = 0; i < _spawnLocations.Length; i++)
        {
            _spawnLocations[i].SpawnWave(_currentWaveCount);
        }
    }

}
