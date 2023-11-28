using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerCommand : I_Command
{
    private TowerObject _receiver;

    private GameplayManager _creditManager;


    public UpgradeTowerCommand(TowerObject pUpgradeTarget, GameplayManager pGameplayManager)
    {
        _receiver = pUpgradeTarget;
        _creditManager = pGameplayManager;

    }

    public bool Execute()
    {
        _receiver.TryUpgradeTower();
        _creditManager.Credits -= _receiver.GetNextUpgradeValues().Cost;
        return true;
    }

    public void Undo()
    {
        _creditManager.Credits += _receiver.GetCurrentValues().Cost;
        _receiver.TryDownGradeTower();
    }
}
