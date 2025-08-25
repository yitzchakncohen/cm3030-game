using System;
using UnityEngine;

public class Rain : MonoBehaviour
{
    private const string ROOF = "roof";
    public event Action OnRainStart;
    public event Action OnRainStop;
    [SerializeField] private ParticleSystem rainFX;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Roof");
        if (other.tag == ROOF)
        {
            rainFX.Stop();
            OnRainStop?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Roof");
        if (other.tag == ROOF)
        {
            rainFX.Play();
            OnRainStart?.Invoke();
        }
    }
}
