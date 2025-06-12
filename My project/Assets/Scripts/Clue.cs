using UnityEngine;

public class Clue : MonoBehaviour
{
    public bool IsScanned => isScanned;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material scannedMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    private bool isScanned = false;

    public void Target()
    {
        meshRenderer.material = highlightMaterial;
    }

    public void Reset()
    {
        meshRenderer.material = defaultMaterial;
    }

    public void SetScanned()
    {
        isScanned = true;
        meshRenderer.material = scannedMaterial;
    }
}
