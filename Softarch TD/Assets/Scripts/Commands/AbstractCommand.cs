using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class Command
{
    protected MonoBehaviour behaviour;

    public Command(MonoBehaviour pBehaviour)
    {
        this.behaviour = pBehaviour;
    }

    public abstract bool Execute();

    public virtual void Undo()
    {

    }

    public virtual void SaveBackupData()
    {

    }
}

public class CommandHistory
{
    private Stack<Command> _history =  new Stack<Command>();

    public void PushCommand(Command pCommand)
    {
        _history.Push(pCommand);
    }

    public Command PopCommand()
    {
        return _history.Pop();
    }

    public bool HasCommands()
    {
        return _history.Count > 0;
    }

}
