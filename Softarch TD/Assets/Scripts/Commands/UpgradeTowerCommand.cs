using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An <see cref="I_Command"/> that attempts to upgrade a <see cref="TowerObject"/>, and then subtracts the corresponding credit value from the <see cref="GameplayManager"/>
/// </summary>
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
        _creditManager.Credits -= _receiver.GetCurrentValues().Cost;
        return true;
    }

    public void Undo()
    {
        _receiver.TryDownGradeTower();
        _creditManager.Credits += _receiver.GetNextUpgradeValues().Cost;
    }
}
