using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
