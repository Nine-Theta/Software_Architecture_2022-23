using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates a UI slider on a canvas with the intention of displaying an object's health.
/// </summary>
/// <remarks>Used by <see cref="EnemyObject"/>s and <see cref="BaseManager"/>.</remarks>
public class HealthbarVisual : MonoBehaviour
{
    private float _sliderMult;

    [SerializeField]
    private Slider _slider;

    public void Initialize(float pMaxHealth)
    {
        _sliderMult = 1 / pMaxHealth;
    }

    public void UpdateHealth(float pValue)
    {
        _slider.value = pValue * _sliderMult;
    }
}
