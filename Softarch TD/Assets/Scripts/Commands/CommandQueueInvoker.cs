using System.Collections.Generic;

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
            _history.Push(_commands.Dequeue());
        }
    }

    public void ExecuteNextCommand()
    {
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
