using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander
{
    private Stack<I_Command> _history = new Stack<I_Command>();

    //Executes the command, adds it to the history stack if it can be undone
    public void ExecuteCommand(I_Command pCommand)
    {
        if (pCommand.Execute())
            PushCommand(pCommand);
    }

    public void UndoCommand()
    {
        if (HasCommands())
        {
            PopCommand().Undo();
        }
    }

    private void PushCommand(I_Command pCommand)
    {
        _history.Push(pCommand);
    }

    private I_Command PopCommand()
    {
        return _history.Pop();
    }

    public bool HasCommands()
    {
        return _history.Count > 0;
    }
}
