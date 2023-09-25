using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Command
{
    bool Execute();

    void Undo();
}

public class CommandHistory
{
    private Stack<I_Command> _history = new Stack<I_Command>();

    public void PushCommand(I_Command pCommand)
    {
        _history.Push(pCommand);
    }

    public I_Command PopCommand()
    {
        return _history.Pop();
    }

    public bool HasCommands()
    {
        return _history.Count > 0;
    }

}
