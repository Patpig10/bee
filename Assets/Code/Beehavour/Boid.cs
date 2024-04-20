using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float mass = 1.0f;
    public float maxSpeed = 20.0f;
    public float maxForce = 10.0f;
    public float damping = 0.01f;

    public Vector3 velocity;
    public Vector3 acceleration;

    void Update()
    {
        Vector3 steeringForce = CalculateSteeringForce();
        ApplyForce(steeringForce);
        UpdatePositionAndRotation();
    }

    Vector3 CalculateSteeringForce()
    {
        Vector3 totalForce = Vector3.zero;

        // Calculate other behaviors and accumulate forces here

        return totalForce;
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
        acceleration = Vector3.ClampMagnitude(acceleration, maxForce);
    }

    public void UpdatePositionAndRotation()
    {
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(velocity.normalized);
        }

        acceleration = Vector3.zero;
    }

    public void Arrive(Vector3 target)
    {
        Vector3 desiredVelocity = (target - transform.position).normalized * maxSpeed;
        Vector3 steeringForce = desiredVelocity - velocity;
        ApplyForce(steeringForce);
    }

    public void StopMoving()
    {
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }
}
