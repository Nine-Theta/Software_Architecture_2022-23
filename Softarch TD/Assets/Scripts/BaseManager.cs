using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the functionality of the base that <see cref="EnemyObject"/>s target.
/// </summary>
[SelectionBase]
public class BaseManager : MonoBehaviour
{
    [SerializeField]
    private float _baseHealth = 100f;

    [SerializeField]
    private HealthbarVisual _healthbarVisual;

    public EventPublisher<float> BaseHealthUpdated = new EventPublisher<float>();
    public EventPublisher BaseDeath = new EventPublisher();

    private void Start()
    {
        _healthbarVisual.Initialize(_baseHealth);

        BaseHealthUpdated.Subscribe(_healthbarVisual.UpdateHealth);
    }


    public float GetBaseHealth() { return _baseHealth; }

    [Button]
    public void OneDamage() { DamageBase(1); }
    [Button]
    public void Kill() { DamageBase(_baseHealth); }

    public void DamageBase(float pDamage)
    {
        _baseHealth -= pDamage;

        BaseHealthUpdated.Publish(_baseHealth);

        if (_baseHealth <= 0)
            BaseDeath.Publish();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        //Debug.Log(other.name + " Has entered the Forbidden Zone(tm)");
    }
}
