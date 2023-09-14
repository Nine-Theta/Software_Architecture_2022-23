using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    public EnemySpawnFactory EnemySpawner;

    [SerializeField]
    private WaveScriptable _currentWave;
    private EnemyGroupScriptable _currentGroup;
    private int _groupCounter = 0; //which group is being handled right now

    public event System.Action OnWaveComplete;

    private void Start()
    {
        StartWave();
    }

    public void LoadWave(WaveScriptable pNewWave, bool pStartWave = true)
    {
        _currentWave = pNewWave;

        if (pStartWave)
        {
            HandleGroup(_currentWave.Wave.First());
            _groupCounter = 1;
        }
    }

    public void StartWave()
    {
        if (_currentWave == null) throw new System.Exception("Tried starting a wave, but no wave Loaded");

        HandleGroup(_currentWave.Wave.First());
        _groupCounter = 1;
    }

    public void OverideCooldown()
    {
        //Todo
    }

    private void HandleGroup(EnemyGroupScriptable pGroup)
    {
        _currentGroup = pGroup;

        EnemySpawner.SpawnEnemyGroup(pGroup, Vector3.zero, Quaternion.identity);

        //_currentGroup.SpawnStrategy.SpawnGroup(pGroup, this);
        _currentGroup.SpawnStrategy.OnSpawningComplete += ProgressWave;
    }

    private void ProgressWave()
    {
        Debug.Log("Wave Progress");

        _currentGroup.SpawnStrategy.OnSpawningComplete -= ProgressWave;

        _groupCounter++;

        if(_groupCounter <= _currentWave.Wave.Count)
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

        HandleGroup(_currentWave.Wave[_groupCounter - 1]);
    }
}
