using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles all of the UI functions. Disabling buttons, displaying values, showing panels, that sort of stuff.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameplayManager _gameplayManager;

    [SerializeField]
    private TextMeshProUGUI _creditUI;

    [SerializeField]
    private TextMeshProUGUI _waveCountUI;

    [SerializeField]
    private GameObject _pauseScreen;

    [SerializeField]
    private SimpleTimerScript _waveCountDown;

    [SerializeField]
    private GameObject _EndScreen;
    [SerializeField]
    private GameObject _WonPanel;
    [SerializeField]
    private GameObject _LostPanel;


    /////

    [Header("BuildUI"), HorizontalLine(color: EColor.Red)]
    [SerializeField]
    public Button _towerButton;
    [SerializeField]
    public Button _foundationButton;
    [SerializeField]
    public Button _upgradeButton;

    [SerializeField]
    private GameObject _towerSelectionTab;

    [SerializeField]
    private GameObject _buildTowerStatsPanel;

    [SerializeField]
    private List<GameObject> _towers;

    [SerializeField]
    private List<TextMeshProUGUI> _buildTowerStats;

    /////

    [Header("UpgradeUI"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private GameObject _towerViewCam;

    [SerializeField]
    private float _camRotationSpeed = 30f;
    private float _oldMouseX = 0f;

    [SerializeField]
    private GameObject _upgradePanel;

    [SerializeField]
    private Button _upgradePanelButton;

    [SerializeField]
    private List<TextMeshProUGUI> _selectedTowerStats;
    [SerializeField]
    private List<TextMeshProUGUI> _towerUpgradedStats;

    private Transform _viewingTower = null;

    private void Awake()
    {
        _gameplayManager.CreditsUpdated.Subscribe(OnCreditsUpdated);
        Debug.Log("credits updated");
    }

    private void OnCreditsUpdated(int pValue)
    {
        _creditUI.text = pValue.ToString();

        Debug.Log("credit event");
    }

    public void UpdateWaveDisplay(int pCurrentWave, int pTotalWaves)
    {
        _waveCountUI.text = (pCurrentWave.ToString() + "/" + pTotalWaves.ToString());
    }

    public void SelectTowerButton(Button pSelected)
    {
        for (int i = 0; i < _towers.Count; i++)
        {
            _towers[i].GetComponent<Button>().interactable = true;
        }

        if (pSelected == null) return;
        pSelected.interactable = false;
    }

    public void DisplayTowerBuildUI()
    {
        _towerSelectionTab.SetActive(true);
        _buildTowerStatsPanel.SetActive(true);
        HideUpgradeUI();

        _towerButton.interactable = false;
        _foundationButton.interactable = true;
    }

    public void HideTowerBuildUI()
    {
        _towerSelectionTab.SetActive(false);

        _buildTowerStatsPanel.SetActive(false);

        _towerButton.interactable = true;
    }

    public void BuildFoundationUI()
    {
        _foundationButton.interactable = false;
        HideTowerBuildUI();
        HideUpgradeUI();
    }

    public void SelectUpgradeButton()
    {
        HideTowerBuildUI();
        _foundationButton.interactable = true;
        _upgradeButton.interactable = false;
    }

    public void DisplayBuildTowerStats(TowerScriptable pTower)
    {
        _buildTowerStats[0].text = pTower.GetName;
        _buildTowerStats[1].text = pTower.TowerRankValues[0].Damage.ToString();
        _buildTowerStats[2].text = pTower.TowerRankValues[0].Range.ToString();
        _buildTowerStats[3].text = pTower.TowerRankValues[0].Cooldown.ToString();
        _buildTowerStats[4].text = pTower.TowerRankValues[0].Cost.ToString();
    }

    public void DisplayUpgradeUI(TowerObject pTower)
    {
        ViewTower(pTower);

        _upgradePanelButton.interactable = (pTower.CanUgrade() && pTower.GetNextUpgradeValues().Cost < _gameplayManager.Credits);

        _upgradePanel.SetActive(true);
    }

    public void HideUpgradeUI()
    {
        _upgradePanel.SetActive(false);

        if (_viewingTower != null)
            SetLayersInChildren(_viewingTower, LayerMask.NameToLayer("Default"));

        _upgradeButton.interactable = true;
    }

    public void DragRotateTowerCam()
    {
        float delta = Input.mousePosition.x - _oldMouseX;
        _oldMouseX = Input.mousePosition.x;
        _towerViewCam.transform.Rotate(new Vector3(0, delta * Time.deltaTime * _camRotationSpeed, 0));
    }

    private void ViewTower(TowerObject pTower)
    {
        if (_viewingTower != null)
            SetLayersInChildren(_viewingTower, LayerMask.NameToLayer("Default"));

        _viewingTower = pTower.GetModel().transform;

        _towerViewCam.transform.position = _viewingTower.position;

        SetLayersInChildren(_viewingTower, LayerMask.NameToLayer("3D View"));

        RefreshTowerValues();
    }

    public void RefreshTowerValues()
    {
        if (_viewingTower == null) return;

        TowerObject tower = _viewingTower.gameObject.GetComponentInParent<TowerObject>();

        _selectedTowerStats[0].text = tower.GetCurrentRank().ToString();
        _selectedTowerStats[1].text = tower.GetCurrentValues().Damage.ToString();
        _selectedTowerStats[2].text = tower.GetCurrentValues().Range.ToString();
        _selectedTowerStats[3].text = tower.GetCurrentValues().Cooldown.ToString();

        int nextRank = tower.GetCurrentRank();

        if (tower.CanUgrade())
            nextRank += 1;

        _towerUpgradedStats[0].text = nextRank.ToString();
        _towerUpgradedStats[1].text = tower.GetNextUpgradeValues().Damage.ToString();
        _towerUpgradedStats[2].text = tower.GetNextUpgradeValues().Range.ToString();
        _towerUpgradedStats[3].text = tower.GetNextUpgradeValues().Cooldown.ToString();
        _towerUpgradedStats[4].text = tower.GetNextUpgradeValues().Cost.ToString();
    }

    //Not a great solution
    private void SetLayersInChildren(Transform pObject, LayerMask pLayer)
    {
        pObject.gameObject.layer = pLayer;
        Transform[] children = pObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Count(); i++)
        {
            children[i].gameObject.layer = pLayer;
        }
    }

    public void TogglePauseScreen()
    {
        _pauseScreen.SetActive(!_pauseScreen.activeSelf);
        _gameplayManager.PauseGame(_pauseScreen.activeSelf);
    }

    public void StartWaveCountdownTimer(int pTime)
    {
        _waveCountDown.gameObject.SetActive(true);
        _waveCountDown.StartTimer(pTime);
    }

    public void ShowGameEndPanel(bool pWonGame)
    {
        if (_EndScreen.activeSelf)
            return;
        _EndScreen.SetActive(true);
        _WonPanel.SetActive(pWonGame);
        _LostPanel.SetActive(!pWonGame);
    }
}
