using System;

/// <summary>
/// Set a single <see cref="I_Command"/> and execute/undo it;
/// </summary>
/// <remarks>see also <see cref="CommandListInvoker"/> and <see cref="CommandQueueInvoker"/></remarks>
public class CommandInvoker
{
    private I_Command _command;

    public CommandInvoker(I_Command pCommand)
    {
        _command = pCommand;
    }

    public void ExecuteCommand()
    {
        _command.Execute();
    }

    public void UndoCommand()
    {
        _command.Undo();
    }

    public void SetCommand(I_Command pCommand)
    {
        _command = pCommand;
    }
}
