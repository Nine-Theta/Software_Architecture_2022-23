using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Interface for commands following the Command pattern
/// </summary>
public interface I_Command
{
    bool Execute();

    void Undo();
}