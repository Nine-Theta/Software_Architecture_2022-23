using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// ScriptableObject which contains the instantiation data for a tower variant.
/// Has a List of <see cref="TowerValues"/> for the tower's stats and model per upgrade stage.
/// Also contains a List of <see cref="DebuffScriptable"/>s that are applied to the enemy on attack.
/// </summary>
/// <remarks>An <see cref="I_Containable"/> used by <see cref="TowerObject"/>.</remarks>
[CreateAssetMenu(fileName = "TowerScriptable", menuName = "ScriptableObjects/Tower")]
public class TowerScriptable : ScriptableObject, I_Containable
{
    [SerializeField]
    private string _name = "tower";

    public AbstractAttackStrategy AttackStrategy; //which enemy to attack

    [Description("[0] is base values, everything else is upgrades")]
    public List<TowerValues> TowerRankValues;

    public string GetName { get { return _name; } }
    public int CreationCost { get { return TowerRankValues[0].Cost; } }

    //To be aplied to Enemy
    public List<DebuffScriptable> Debuffs = new List<DebuffScriptable>();

    public GameObject GetModel
    {
        get { return GetRankModel(0); }
    }

    public GameObject GetRankModel(int pRank)
    {
        if (pRank < 0)
            pRank = 0;
        else if (pRank >= TowerRankValues.Count)
            pRank = TowerRankValues.Count - 1;

        return TowerRankValues[pRank].TowerModel;
    }
}
