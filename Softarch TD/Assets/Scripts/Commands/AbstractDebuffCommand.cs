using System;

public enum DebuffType { SUBTRACT, MULTIPLY, BLEED }

[Serializable]
public abstract class AbstractDebuffCommand : I_Command
{
    protected float Strength = 0f;

    protected float Duration = 1;

    protected float TickSpeed = 1;

    protected float Value;

    public abstract bool Execute();

    public abstract void Undo();
}