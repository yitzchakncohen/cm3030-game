using UnityEngine;
using UnityEngine.Events;

public class HintBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject associatedHint;
    [SerializeField] private UnityEvent playAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        associatedHint.GetComponent<Animator>().SetTrigger("Trigger");
        playAudio?.Invoke();
    }
}
