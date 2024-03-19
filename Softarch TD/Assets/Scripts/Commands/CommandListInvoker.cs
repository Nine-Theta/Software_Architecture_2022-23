using System.Collections.Generic;

/// <summary>
/// A list of <see cref="I_Command"/>s. Allows for the addition and removal of commands, but will Execute/Undo all of them at once.
/// </summary>
/// <remarks>see also <see cref="CommandInvoker"/> and <see cref="CommandQueueInvoker"/></remarks>
public class CommandListInvoker
{
    private List<I_Command> _commands;


    public CommandListInvoker(List<I_Command> pCommands)
    {
        _commands = pCommands;
    }

    public void ExecuteCommands()
    {
        for (int i = 0; i < _commands.Count; i++)
        {
            _commands[i].Execute();
        }
    }

    public void UndoCommands()
    {
        for (int i = 0; i < _commands.Count; i++)
        {
            _commands[i].Undo();
        }
    }

    public void AddCommand(I_Command pCommand)
    {
        _commands.Add(pCommand);
    }

    public void RemoveCommand(I_Command pCommand)
    {
        if (_commands.Contains(pCommand))
            _commands.Remove(pCommand);
    }

    public bool HasCommands()
    {
        return _commands.Count > 0;
    }
}
