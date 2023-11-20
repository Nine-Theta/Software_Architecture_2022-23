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

    [SerializeField]
    private float _baseHealth = 100f;

    public EventPublisher<float> BaseHealthUpdated = new EventPublisher<float>();
    public EventPublisher BaseDeath = new EventPublisher();


    public float GetBaseHealth() { return _baseHealth; }

    [Button]
    public void OneDamage() { DamageBase(1); }
    [Button]
    public void Kill() { DamageBase(_baseHealth); }

    public void DamageBase(float pDamage)
    {
        _baseHealth -= pDamage;

        if (_baseHealth <= 0)
            BaseDeath.Publish();
        else
            BaseHealthUpdated.Publish(_baseHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        Debug.Log(other.name + " Has entered the Forbidden Zone(tm)");
    }
}
