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
        // Calculate steering force based on behaviors (not shown here)
        Vector3 steeringForce = CalculateSteeringForce();

        // Apply the calculated force
        ApplyForce(steeringForce);

        // Update position and rotation based on velocity
        UpdatePositionAndRotation();
    }

    Vector3 CalculateSteeringForce()
    {
        // Placeholder method to calculate steering forces (e.g., cohesion, separation, alignment)
        return Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        // Update acceleration based on the applied force
        acceleration += force / mass;
        acceleration = Vector3.ClampMagnitude(acceleration, maxForce);
    }
    public void Arrive(Vector3 target)
    {
        Vector3 desiredVelocity = (target - transform.position).normalized * maxSpeed;
        Vector3 steeringForce = desiredVelocity - velocity;
        ApplyForce(steeringForce);
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
            // Calculate rotation towards the velocity direction
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);

            // Debug logs to inspect rotation values
            Debug.Log("Target Rotation: " + targetRotation.eulerAngles);
            Debug.Log("Current Rotation: " + transform.rotation.eulerAngles);

            // Smoothly interpolate rotation using Quaternion.Lerp
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * damping);
        }

        // Reset acceleration
        acceleration = Vector3.zero;
    }

}
