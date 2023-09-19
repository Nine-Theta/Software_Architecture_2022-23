using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerCommand : Command
{
    private GameObject _towerBackup;

    public BuildTowerCommand(TowerBuildScript pBehaviour) : base(pBehaviour)
    {
    }

    public override bool Execute()
    {
        _towerBackup = (behaviour as TowerBuildScript).BuildTower();
        return true;
    }

    public override void Undo()
    {
        (behaviour as TowerBuildScript).RemoveTower(_towerBackup);
    }
}
