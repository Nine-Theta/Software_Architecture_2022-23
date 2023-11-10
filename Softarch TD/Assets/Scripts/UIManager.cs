using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _creditUI;

    private int _creditTracker;


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

    private TowerObject _viewingTower;


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

    public int CreditVisual
    {
        get { return _creditTracker; }
        set
        {
            _creditTracker = value;
            _creditUI.text = value.ToString();
        }
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

        _upgradePanelButton.interactable = (pTower.CanUgrade() && pTower.GetNextUpgradeValues().Cost < _creditTracker);

        _upgradePanel.SetActive(true);
    }

    public void HideUpgradeUI()
    {
        _upgradePanel.SetActive(false);

        if(_viewingTower != null)
            _viewingTower.GetModel().layer = LayerMask.NameToLayer("Default");

        _towerButton.interactable = true;
    }

    public void DragRotateTowerCam()
    {
        float delta = Input.mousePosition.x - _oldMouseX;
        _oldMouseX = Input.mousePosition.x;
        _towerViewCam.transform.Rotate(new Vector3(0,delta * Time.deltaTime * _camRotationSpeed,0));
    }

    private void ViewTower(TowerObject pTower)
    {
        if (_viewingTower != null)
            _viewingTower.GetModel().layer = LayerMask.NameToLayer("Default");

        _viewingTower = pTower;

        _towerViewCam.transform.position = pTower.transform.position;

        pTower.GetModel().layer = LayerMask.NameToLayer("3D View");

        _towerStats[0].text = pTower.GetCurrentValues().Damage.ToString();
        _towerStats[1].text = pTower.GetCurrentValues().Range.ToString();
        _towerStats[2].text = pTower.GetCurrentValues().Cooldown.ToString();

        _upgradeStats[0].text = pTower.GetNextUpgradeValues().Damage.ToString();
        _upgradeStats[1].text = pTower.GetNextUpgradeValues().Range.ToString();
        _upgradeStats[2].text = pTower.GetNextUpgradeValues().Cooldown.ToString();
        _upgradeStats[3].text = pTower.GetNextUpgradeValues().Cost.ToString();
    }
}
