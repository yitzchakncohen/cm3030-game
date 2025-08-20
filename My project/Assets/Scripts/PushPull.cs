using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;


public class PushPull : Movable
{
    [SerializeField]
    private float pushForce = 5f;

    private FixedJoint joint;
    private InputManager inputManager;
    private Vector2 moveVector;
    protected override void Start()
    {
        base.Start();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();

        inputManager.OnMoveInput += OnMoveInput;
        joint = GetComponent<FixedJoint>();
    }

    private void OnMoveInput(Vector2 moveInput)
    {
        moveVector = moveInput.normalized;
    }

    public override void Reset()
    {
        base.Reset();
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        moveVector = Vector2.zero;
        joint.connectedBody = null;
    }

    public override void Grab(Transform transform)
    {
        base.Grab(transform);
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        joint.connectedBody = transform.gameObject.GetComponentInParent<Rigidbody>();

        // Force Joint update code referenced from - https://discussions.unity.com/t/how-to-re-initialize-a-joint/817087/10
        joint.autoConfigureConnectedAnchor = false;
        joint.autoConfigureConnectedAnchor = true;
    }

    public override void MoveToTarget(Transform target, float grabForce)
    {
    }
}
