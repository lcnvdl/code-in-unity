using UnityEngine;

public class ExplodeForce : MonoBehaviour
{
    public float radius = 5f;

    public float yOffset = 1f;

    public float maxDistance = 10f;

    //public Vector3 offset = Vector3.zero;

    public float force = 1f;

    public virtual void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.isStatic)
            {
                continue;
            }

            var body = collider.GetComponent<Rigidbody>();
            if (body == null)
            {
                continue;
            }

            this.ExplodeBody(body);
        }
    }

    protected virtual void ExplodeBody(Rigidbody body)
    {
        body.AddExplosionForce(this.force, transform.position, radius, yOffset, ForceMode.Impulse);
    }
}
