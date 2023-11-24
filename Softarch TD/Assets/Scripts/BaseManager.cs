using JetBrains.Annotations;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class BaseManager : MonoBehaviour
{
    [SerializeField]
    private float _baseHealth = 100f;

    private float _healhSliderMult;

    [SerializeField]
    private Slider _healthSilder;

    public EventPublisher<float> BaseHealthUpdated = new EventPublisher<float>();
    public EventPublisher BaseDeath = new EventPublisher();

    private void Start()
    {
        _healhSliderMult = 1 / _baseHealth;
        _healthSilder.value = _baseHealth * _healhSliderMult;
    }


    public float GetBaseHealth() { return _baseHealth; }

    [Button]
    public void OneDamage() { DamageBase(1); }
    [Button]
    public void Kill() { DamageBase(_baseHealth); }

    public void DamageBase(float pDamage)
    {
        _baseHealth -= pDamage;

        _healthSilder.value = _baseHealth * _healhSliderMult;

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
