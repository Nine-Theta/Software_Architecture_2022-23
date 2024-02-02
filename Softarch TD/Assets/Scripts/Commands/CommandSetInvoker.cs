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
