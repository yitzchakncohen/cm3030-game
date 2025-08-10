using UnityEngine;

public class Pickup : Movable
{
    public override void Reset()
    {
        base.Reset();
        transform.SetParent(null);
    }

    public override void Grab(Transform transform)
    {
        base.Grab(transform);
        transform.SetParent(transform);
        transform.position = transform.position;
    }

    public override void MoveToTarget(Transform target, float grabForce)
    {
        if (Vector3.Distance(target.position, transform.position) > moveDistance)
        {
            Vector3 pickupMoveDirection = (target.position - transform.position).normalized;
            rigidBody.AddForce(pickupMoveDirection * grabForce);
        }
    }
}
