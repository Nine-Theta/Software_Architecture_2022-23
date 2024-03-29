using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// deprecated <see cref="I_Command"/> that was used in a previous version of tower construction
/// </summary>
public class ConstructionCommand : I_Command
{
    private AbstractInstanceFactory _receiver;

    private Vector3 _position;

    private AbstractContainerObject _towerBackup;

    public ConstructionCommand(AbstractInstanceFactory pReceiver, Vector3 pPosition)
    {
        this._receiver = pReceiver;
        _position = pPosition;
    }

    public bool Execute()
    {
        _towerBackup = _receiver.CreateInstance(_position, Quaternion.identity);
        return true;
    }

    public void Undo()
    {
        _receiver.DeleteInstance(_towerBackup);
    }
}
