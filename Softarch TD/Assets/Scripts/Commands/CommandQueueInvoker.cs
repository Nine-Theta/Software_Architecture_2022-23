using System.Collections.Generic;

/// <summary>
/// Contains a Queue of <see cref="I_Command"/>s. Allows for the addition of a single command and execution of either a single or all commands on a FIFO basis. Undoing is on a FILO basis;
/// </summary>
/// <remarks>see also <see cref="CommandInvoker"/> and <see cref="CommandListInvoker"/></remarks>
public class CommandQueueInvoker
{
    private Queue<I_Command> _commands = new Queue<I_Command>();

    private Stack<I_Command> _history = new Stack<I_Command>();

    public void AddCommand(I_Command pCommand)
    {
        _commands.Enqueue(pCommand);
    }

    public void ExecuteAllCommands()
    {
        for (int i = 0; i < _commands.Count; i++)
        {
            _commands.Peek().Execute();
            _history.Push(_commands.Dequeue());
        }
    }

    public void ExecuteNextCommand()
    {
        _commands.Peek().Execute();
        _history.Push(_commands.Dequeue());
    }

    public void UndoCommand()
    {
        if (HasHistory())
        {
            _history.Pop().Undo();
        }
    }

    public bool HasCommands()
    {
        return _commands.Count > 0;
    }

    public bool HasHistory()
    {
        return _history.Count > 0;
    }
}
