using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// A simple script that controls a TMP text attached to a canvas with the intention of displaying pop-up values.
/// </summary>
/// <remarks>Used by <see cref="EnemyObject"/>.</remarks>
public class PopUpControlScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private float _duration;

    public void Initialize(float pPopupValue, float pDuration, Color pColor)
    {
        Initialize((pPopupValue >= 0 ? "+" : "-" )+ pPopupValue.ToString(), pDuration, pColor);
    }

    public void Initialize(string pPopupText, float pDuration, Color pColor)
    {
        _text.text = pPopupText;
        _text.color = pColor;
        _duration = 1/(pDuration*100f);
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        Color c = _text.color;
        for (float alpha = 1f; alpha > 0f; alpha -= _duration)
        {
            _text.transform.Translate(0,_duration,0);
            c.a = alpha;
            _text.color = c;
            yield return null;
        }
        Destroy(gameObject);
    }

    [Button]
    public void TestFade()
    {
        Initialize("E", 1, Color.yellow);
    }
}
