using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameplayManager _gameplayManager;

    [SerializeField]
    private TextMeshProUGUI _creditUI;

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
    private List<GameObject> _towers;

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
    private List<TextMeshProUGUI> _towerStats;
    [SerializeField]
    private List<TextMeshProUGUI> _upgradeStats;

    private Transform _viewingTower = null;

    private void Awake()
    {
        _gameplayManager.CreditsUpdated.Subscribe(OnCreditsUpdated);
    }

    private void OnCreditsUpdated(int pValue)
    {
        _creditUI.text = pValue.ToString();
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

        _towerButton.interactable = false;
        _foundationButton.interactable = true;
        _upgradeButton.interactable = true;
    }

    public void HideTowerBuildUI()
    {
        _towerSelectionTab.SetActive(false);

        _towerButton.interactable = true;
    }

    public void BuildFoundationUI()
    {
        _foundationButton.interactable = false;
        HideTowerBuildUI();
        _upgradeButton.interactable = true;
    }

    public void SelectUpgradeButton()
    {
        HideTowerBuildUI();
        _foundationButton.interactable = true;
        _upgradeButton.interactable = false;
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

        _towerButton.interactable = true;
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

        _towerStats[0].text = tower.GetCurrentRank().ToString();
        _towerStats[1].text = tower.GetCurrentValues().Damage.ToString();
        _towerStats[2].text = tower.GetCurrentValues().Range.ToString();
        _towerStats[3].text = tower.GetCurrentValues().Cooldown.ToString();

        int nextRank = tower.GetCurrentRank();

        if (tower.CanUgrade())
            nextRank += 1;

        _upgradeStats[0].text = nextRank.ToString();
        _upgradeStats[1].text = tower.GetNextUpgradeValues().Damage.ToString();
        _upgradeStats[2].text = tower.GetNextUpgradeValues().Range.ToString();
        _upgradeStats[3].text = tower.GetNextUpgradeValues().Cooldown.ToString();
        _upgradeStats[4].text = tower.GetNextUpgradeValues().Cost.ToString();
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
}
