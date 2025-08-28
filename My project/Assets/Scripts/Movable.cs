using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Movable : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material grabbedMaterial;
    [SerializeField] private GameObject mesh;
    [SerializeField] protected Rigidbody rigidBody;
    [SerializeField] protected float freeDamping = 1f;
    [SerializeField] protected float grabbedDamping = 10f;
    [SerializeField] protected float moveDistance = 0.5f;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        defaultMaterial = mesh.GetComponentInChildren<Renderer>().material;
    }

    public void Target()
    {
        SetMaterial(highlightMaterial);
    }

    public virtual void Reset()
    {
        SetMaterial(defaultMaterial);
        rigidBody.useGravity = true;
        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.linearDamping = freeDamping;
    }

    public virtual void Grab(Transform transform)
    {
        SetMaterial(grabbedMaterial);
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.linearDamping = grabbedDamping;
    }

    public abstract void MoveToTarget(Transform target, float grabForce);


    protected void SetMaterial(Material material)
    {
        foreach (var meshRenderer in mesh.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = material;
        }
    }
}
