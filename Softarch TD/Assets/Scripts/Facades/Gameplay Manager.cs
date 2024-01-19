using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private int _credits = 20;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private NavigationManager _navManager;

    [SerializeField]
    private EnemySpawnManager _spawnManager;

    private TowerObject _selectedTower;


    public AbstractInstanceFactory ConstructionFactory;

    public Commander ConstructionCommander { get; private set; } = new Commander();

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
        _credits = SceneSettings.Instance.GetSceneCredits();

        _spawnManager = SceneSettings.Instance.GetSceneSpawnManager();

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

    public void StartNextWave()
    {
        if (_navManager.IsLayoutValid())
        {

            _uiManager.UpdateWaveDisplay(_spawnManager.CurrentWaveCount + 1, _spawnManager.TotalWaves);

            _spawnManager.SpawnNextWave();
        }
        else
        {
            Debug.Log("Level Layout not Valid, gotta give the enemies a chance, ya know");
        }
    }

    private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
    {
        GetSceneValues();
    }
}
