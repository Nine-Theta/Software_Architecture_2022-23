using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractDebuffCommand : AbstractDebuffCommand
{
    public SubtractDebuffCommand(float pStrength, float pDuration, float pValue)
    {
        Strength = pStrength;
        Duration = pDuration;

        Value = pValue;
    }

    public override bool Execute()
    {
        Value -= Strength;
        return true;
    }

    public override void Undo()
    {
        Value += Strength;
    }
}
