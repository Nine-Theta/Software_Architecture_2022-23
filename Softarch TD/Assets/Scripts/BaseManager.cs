using JetBrains.Annotations;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BaseManager : MonoBehaviour
{
    public Vector3 GetEnemyTarget
    {
        get { return transform.position; }
    }    

    public int BaseHealth = 100;

    [Button]
    public void OneDamage() { DamageBase(1); }
    [Button]
    public void Kill () { DamageBase(BaseHealth); }

    public void DamageBase(int pDamage)
    {
        BaseHealth -= pDamage;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " Has entered the Forbidden Zone(tm)");
    }
}
