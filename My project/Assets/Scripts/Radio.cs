using System;
using System.Collections;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public event Action OnPlayRadioNoise;
    public event Action OnPlayIntroVoiceSequence;
    private float delay = 1f;
    public void PlayStartRadio()
    {
        StartCoroutine(RadioRoutine());
    }

    private IEnumerator RadioRoutine()
    {
        OnPlayRadioNoise?.Invoke();
        yield return new WaitForSeconds(delay);
        OnPlayIntroVoiceSequence?.Invoke();
    }
}
