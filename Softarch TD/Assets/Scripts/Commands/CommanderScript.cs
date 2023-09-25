using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander
{
    private CommandHistory _history = new CommandHistory();

    public void ExecuteCommand(I_Command pCommand)
    {
        if (pCommand.Execute())
            _history.PushCommand(pCommand);
    }

    public void UndoCommand()
    {
        if (_history.HasCommands())
        {
            _history.PopCommand().Undo();
        }
    }
}
