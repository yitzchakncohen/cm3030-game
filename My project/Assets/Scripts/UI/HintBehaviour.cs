using UnityEngine;

public class HintBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject associatedHint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        associatedHint.GetComponent<Animator>().SetTrigger("Trigger");
    }
}
