using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Command
{
    bool Execute();

    void Undo();
}