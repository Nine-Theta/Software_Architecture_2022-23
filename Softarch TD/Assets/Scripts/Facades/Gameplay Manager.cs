using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private int _credits = 20;

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
}
