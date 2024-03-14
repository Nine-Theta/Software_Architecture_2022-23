using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTimerScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textField;

    [SerializeField]
    private UnityEvent _TimerDone;

    public void StartTimer(int pTime)
    {
        StartCoroutine(Timer(pTime));
    }

    public void InterruptTimer()
    {
        StopCoroutine(Timer(1));
        gameObject.SetActive(false);
    }

    private void UpdateText(int pTime)
    {
        _textField.text = string.Format("{0:D2}:{1:D2}", pTime / 60, pTime % 60);
    }

    private IEnumerator Timer(int pTime)
    {
        for(int t = pTime; t > 0; t -= 1)
        {
            UpdateText(t);
            yield return new WaitForSecondsRealtime(1f);
        }
        _TimerDone.Invoke();
    }
}
