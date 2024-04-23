using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisperseBehavior : MonoBehaviour
{
    public float dispersalRadius = 5.0f;
    public float separationDistance = 2.0f;

    private Boid boid;

    void Start()
    {
        boid = GetComponent<Boid>();
    }

    public Vector3 CalculateDispersalForce()
    {
        Vector3 dispersalForce = Vector3.zero;
        int count = 0;

        Collider[] colliders = Physics.OverlapSphere(transform.position, dispersalRadius);

        foreach (Collider collider in colliders)
        {
            if (collider != this.GetComponent<Collider>())
            {
                Vector3 toOther = collider.transform.position - transform.position;
                float distance = toOther.magnitude;

                if (distance > 0 && distance < separationDistance)
                {
                    Vector3 separationDirection = -toOther.normalized;
                    dispersalForce += separationDirection / distance;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            dispersalForce /= count;
            dispersalForce = Vector3.ClampMagnitude(dispersalForce, boid.maxForce);
        }

        return dispersalForce;
    }
}