using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private int _credits = 20;

    [SerializeField]
    private EnemySpawnManager _spawnManager;

    [SerializeField]
    private UIManager _uiManager;

    private TowerObject _selectedTower;

    private Commander UpgradeCommander = new Commander();

    public int Credits
    {
        get { return _credits; }
        set
        {
            _credits = value;
            CreditsUpdated.Publish(_credits);
        }
    }

    public EventPublisher<int> CreditsUpdated = new EventPublisher<int>();

    private void Start()
    {
        CreditsUpdated.Publish(Credits);

        SceneManager.sceneLoaded += OnSceneLoaded;
        GetSceneValues();
    }

    public void GetSceneValues()
    {
        _spawnManager = SceneSettings.Instance.GetSceneSpawnManager();

        _spawnManager.SpawningWave.Subscribe(_uiManager.UpdateWaveDisplay);

        _uiManager.UpdateWaveDisplay(0, _spawnManager.TotalWaves);
    }

    public void SetGameSpeed(float pSpeed)
    {
        Time.timeScale = pSpeed;
    }

    public void SetSelectedTower(TowerObject pSelectedTower)
    {
        _selectedTower = pSelectedTower;
    }

    public void UpgradeTower()
    {
        if(_selectedTower.CanUgrade() && _selectedTower.GetNextUpgradeValues().Cost <= _credits)
        {
            UpgradeCommander.ExecuteCommand(new UpgradeTowerCommand(_selectedTower, this));
        }
    }

    public void SellTower()
    {

    }

    public void StartLevel()
    {
        _spawnManager.SpawnNextWave();
    }
    private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
    {
        GetSceneValues();
    }
}
