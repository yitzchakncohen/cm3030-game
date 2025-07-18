using UnityEngine;

public class Clue : MonoBehaviour
{
    private const string GREY_LEVEL_PROPERTY = "greyLevel";
    public bool IsScanned => isScanned;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject scannedEffect;
    private Material material;
    private float highlightAmount = 0.7f;
    private bool isScanned = false;

    private void Awake()
    {
        material = meshRenderer.material;
        material.SetFloat(GREY_LEVEL_PROPERTY, 1f);
    }

    public void Target()
    {
        material.SetFloat(GREY_LEVEL_PROPERTY, highlightAmount);
    }

    public void Reset()
    {
        material.SetFloat(GREY_LEVEL_PROPERTY, 1.0f);
    }

    public void UpdateScan(float percentageComplete)
    {
        material.SetFloat(GREY_LEVEL_PROPERTY, Mathf.Lerp(highlightAmount, 0f, percentageComplete));
    }

    public void SetScanned()
    {
        isScanned = true;
        material.SetFloat(GREY_LEVEL_PROPERTY, 0f);
        scannedEffect.SetActive(true);
    }
}
