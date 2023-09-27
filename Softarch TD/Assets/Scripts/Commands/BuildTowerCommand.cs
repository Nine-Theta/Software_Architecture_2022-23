using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerCommand : I_Command
{
    private TowerFactory _receiver;

    private TowerScriptable _towerToBuild;

    private Vector3 _position;

    private GameObject _towerBackup;

    public BuildTowerCommand(TowerFactory pReceiver, TowerScriptable pTowerType, Vector3 pPosition)
    {
        this._receiver = pReceiver;
        _towerToBuild = pTowerType;
        _position = pPosition;
    }

    public bool Execute()
    {
        _towerBackup = _receiver.CreateInstance(_towerToBuild, _position);
        return true;
    }

    public void Undo()
    {
        _receiver.DeleteInstance(_towerBackup);
    }
}
