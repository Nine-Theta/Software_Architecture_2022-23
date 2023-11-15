using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCreditsCommand : I_Command
{
    private GameplayManager _receiver;

    private int _backupValue;

    public UpdateCreditsCommand(GameplayManager pGameplayManager, int pValue)
    {
        _receiver = pGameplayManager;
        _backupValue = pValue;
    }

    public bool Execute()
    {
        //GameplayManager
        return false;
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
