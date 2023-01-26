using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private WaveScriptable _currentWave;
    private EnemyGroupScriptable _currentGroup;

    public void LoadWave(WaveScriptable pNewWave)
    {
        _currentWave = pNewWave;
    }

    public void StartWave()
    {
        
    }

    public void Start()
    {
        HandleGroup(_currentWave.Wave.First());
    }

    private void HandleGroup(EnemyGroupScriptable pGroup)
    {
        _currentGroup = pGroup;
        _currentGroup.SpawnStrategy.SpawnGroup(pGroup, this);
        _currentGroup.SpawnStrategy.OnSpawningComplete += ProgressWave;
    }

    private void ProgressWave()
    {
        _currentGroup.SpawnStrategy.OnSpawningComplete -= ProgressWave;
        StartCoroutine(GroupCooldown(5));

    }

    private void OnDisable()
    {
        _currentGroup.SpawnStrategy.OnSpawningComplete -= ProgressWave;
    }

    private IEnumerator GroupCooldown(int pSeconds)
    {
        yield return new WaitForSeconds(pSeconds);
        Debug.Log("In Routine");
        //Todo: handle new group
    }
}
