using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material grabbedMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float freeDampning = 1f;
    [SerializeField] private float grabbedDampning = 10f;

    public void Target()
    {
        meshRenderer.material = highlightMaterial;
    }

    public void Reset()
    {
        meshRenderer.material = defaultMaterial;
        rigidBody.useGravity = true;
        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.linearDamping = freeDampning;
    }

    public void Grab()
    {
        meshRenderer.material = grabbedMaterial;
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.linearDamping = grabbedDampning;
    }
}
