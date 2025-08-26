using System;
using System.Collections;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public event Action OnPlayIntroVoiceSequence;

    public void PlayStartRadio()
    {
        OnPlayIntroVoiceSequence?.Invoke();
    }
}
