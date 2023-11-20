using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
