using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    private const string GREY_LEVEL_PROPERTY = "greyLevel";
    public bool IsScanned => isScanned;
    [SerializeField] private List<MeshRenderer> meshRenderers;
    [SerializeField] private GameObject scannedEffect;
    private List<Material> materials;
    private float highlightAmount = 0.7f;
    private bool isScanned = false;

    private void Awake()
    {
        materials = new List<Material>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.SetFloat(GREY_LEVEL_PROPERTY, 1f);
            materials.Add(meshRenderer.material);
        }
    }

    public void Target()
    {
        foreach (Material material in materials) material.SetFloat(GREY_LEVEL_PROPERTY, highlightAmount);
    }

    public void Reset()
    {
        foreach (Material material in materials) material.SetFloat(GREY_LEVEL_PROPERTY, 1.0f);
    }

    public void UpdateScan(float percentageComplete)
    {
        foreach (Material material in materials) material.SetFloat(GREY_LEVEL_PROPERTY, Mathf.Lerp(highlightAmount, 0f, percentageComplete));
    }

    public void SetScanned()
    {
        isScanned = true;
        foreach (Material material in materials) material.SetFloat(GREY_LEVEL_PROPERTY, 0f);
        scannedEffect.SetActive(true);
    }
}
