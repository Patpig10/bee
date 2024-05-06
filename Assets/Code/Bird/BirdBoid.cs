using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBoid : MonoBehaviour
{
    public float mass = 1.0f;
    public float maxSpeed = 5.0f;
    public float maxForce = 1.0f;
    public float damping = 0.1f;

    public float wanderRadius = 5.0f;
    public float wanderDistance = 10.0f;
    public float wanderJitter = 1.0f;

    private Vector3 velocity;
    private Vector3 acceleration;

    void Update()
    {
        // Calculate steering force for flocking and wandering
        Vector3 flockingForce = CalculateFlockingForce();
        Vector3 wanderingForce = CalculateWanderingForce();

        // Combine steering forces
        Vector3 steeringForce = flockingForce + wanderingForce;

        // Apply the combined steering force
        ApplyForce(steeringForce);

        // Update position and rotation based on velocity
        UpdatePositionAndRotation();
    }

    Vector3 CalculateFlockingForce()
    {
        // Placeholder for flocking behavior (cohesion, separation, alignment)
        return Vector3.zero;
    }

    Vector3 CalculateWanderingForce()
    {
        // Calculate a wandering force to create random movement within a range
        Vector3 wanderTarget = transform.position + Random.insideUnitSphere * wanderRadius;
        Vector3 desiredVelocity = (wanderTarget - transform.position).normalized * maxSpeed;
        return desiredVelocity - velocity;
    }

    public void ApplyForce(Vector3 force)
    {
        // Update acceleration based on the applied force
        acceleration += force / mass;
        acceleration = Vector3.ClampMagnitude(acceleration, maxForce);
    }

    public void UpdatePositionAndRotation()
    {
        // Update velocity based on acceleration
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Update position based on velocity
        transform.position += velocity * Time.deltaTime;

        // Update rotation to face the direction of movement (if moving)
        if (velocity.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * damping);
        }

        // Reset acceleration
        acceleration = Vector3.zero;
    }
}
