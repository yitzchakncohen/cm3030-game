using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private float flickerInterval = 5f;
    private float flickerStopsMin = 0.05f;
    private float flickerStopsMax = 0.3f;
    private int flickersMin = 4;
    private int flickersMax = 10;
    [SerializeField] private Light flickeringLight;

    private void Start()
    {
        InvokeRepeating("Flicker", flickerInterval, flickerInterval);
    }

    private void Flicker()
    {
        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        int flickers = Random.Range(flickersMin, flickersMax);
        for (int i = 0; i < flickers; i++)
        {
            flickeringLight.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(flickerStopsMin, flickerStopsMax));
            flickeringLight.gameObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(flickerStopsMin, flickerStopsMax));
        }
    }
}
