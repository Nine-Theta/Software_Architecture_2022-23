using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the values and model for an unspecified tower upgrade stage.
/// </summary>
/// <remarks>Contained in a <see cref="TowerScriptable"/></remarks>
[Serializable]
public class TowerValues
{
    [Tooltip("How much damage the turret does per shot")]
    public float Damage;

    [Tooltip("The Range of the turret in Unity Units (meters)")]
    public float Range;

    [Tooltip("How often the turret shoots per Second")]
    public float Cooldown;

    [Tooltip("How much credits it costs to either build or upgrade the tower")]
    public int Cost;

    [Tooltip("How The tower looks at this stage")]
    public GameObject TowerModel;

    /// <summary>
    /// The values and model for an unspecified tower upgrade stage.
    /// </summary>
    /// <param name="pDamage">How much damage the turret does per shot</param>
    /// <param name="pRange">The Range of the turret in Unity Units (meters)</param>
    /// <param name="pCooldown">How often the turret shoots per Second</param>
    /// <param name="pCost">How much credits it costs to either build or upgrade the tower</param>
    /// <param name="pModel">How The tower looks at this stage</param>
    public TowerValues(float pDamage, float pRange, float pCooldown, int pCost, GameObject pModel)
    {
        Damage = pDamage;
        Range = pRange;
        Cooldown = pCooldown;
        Cost = pCost;
        TowerModel = pModel;
    }

    public TowerValues(TowerValues pOriginal) : this(pOriginal.Damage, pOriginal.Range, pOriginal.Cooldown, pOriginal.Cost, pOriginal.TowerModel) { }
}
