using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unused <see cref="I_Command"/> that Executes a specified method, and Undoes a specified method
/// </summary>
/// <remarks>Probably should remain unused</remarks>
public class ExecuteMethodCommand : I_Command
{
    public delegate void Method();

    private Method _do;
    private Method _undo;


    public ExecuteMethodCommand(Method pExecute, Method pUnexecute)
    {
        _do = pExecute;
        _undo = pUnexecute;
    }

    public bool Execute()
    {
        _do.Invoke();
        return true;
    }

    public void Undo()
    {
        _undo.Invoke();
    }
}
