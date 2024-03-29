using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Handles most of the general game functions during runtime.
/// </summary>
/// <remarks>See <see cref="InputProcessor"/> and <see cref="UIManager"/> for most of the input functions</remarks>
public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private int _credits = 20;

    [SerializeField]
    private float _towerSellMod = .5f;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private NavigationManager _navManager;

    [SerializeField]
    private EnemySpawnManager _spawnManager;


    private TowerObject _selectedTower;

    private bool _canSpawnNextWave = true;


    private float _gameSpeed = 1f;


    public AbstractInstanceFactory ConstructionFactory;

    public CommandInvoker ConstructionCommander { get; private set; } = new CommandInvoker(null);

    private CommandInvoker UpgradeCommander = new CommandInvoker(null);

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        GetSceneValues();
    }

    public void GetSceneValues()
    {
        Credits = SceneSettings.Instance.GetSceneCredits();

        _spawnManager = SceneSettings.Instance.GetSceneSpawnManager();

        _spawnManager.CurrentWaveComplete.Subscribe(OnWaveComplete);
        _spawnManager.AllWavesComplete.Subscribe(OnAllWavesComplete);

        SceneSettings.Instance.GetSceneBase().BaseDeath.Subscribe(OnBaseDestroyed);

        _uiManager.UpdateWaveDisplay(0, _spawnManager.TotalWaves);

        _canSpawnNextWave = true;
    }

    public void SetGameSpeed(float pSpeed)
    {
        _gameSpeed = pSpeed;
        Time.timeScale = pSpeed;
    }

    public void SetSelectedTower(TowerObject pSelectedTower)
    {
        _selectedTower = pSelectedTower;
        UpgradeCommander.SetCommand(new UpgradeTowerCommand(_selectedTower, this));
    }

    public void UpgradeTower()
    {
        if(_selectedTower.CanUgrade() && _selectedTower.GetNextUpgradeValues().Cost <= Credits)
        {
            UpgradeCommander.ExecuteCommand();
            _uiManager.DisplayUpgradeUI(_selectedTower);
        }
    }

    public void SellTower()
    {
        Credits += (int)(_selectedTower.GetCurrentValues().Cost * _towerSellMod);
        Destroy(_selectedTower.gameObject);
        _selectedTower = null;
    }

    public void StartNextWave()
    {
        if (_navManager.IsLayoutValid())
        {
            if (!_canSpawnNextWave) //TODO: make sure this works
                return;

            _uiManager.UpdateWaveDisplay(_spawnManager.CurrentWaveCount + 1, _spawnManager.TotalWaves);

            _spawnManager.SpawnNextWave();
        }
        else
        {
            Debug.Log("Level Layout not Valid, gotta give the enemies a chance, ya know");
        }
    }

    public void PauseGame(bool pPause)
    {
        if (pPause)
            Time.timeScale = 0;
        else
            SetGameSpeed(_gameSpeed);
    }

    public void QuitGame()
    {
        SceneLoadManager.Instance.QuitGame();
    }

    public void ReloadScene()
    {
        SceneLoadManager.Instance.ReloadLevel();
    }

    private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
    {
        GetSceneValues();
    }

    private void OnWaveComplete(int pWave)
    {
        _uiManager.StartWaveCountdownTimer(20);
    }

    private void OnAllWavesComplete()
    {
        SetGameSpeed(0);
        _uiManager.ShowGameEndPanel(true);
    }

    private void OnBaseDestroyed()
    {
        SetGameSpeed(0);
        _uiManager.ShowGameEndPanel(false);
    }
}
