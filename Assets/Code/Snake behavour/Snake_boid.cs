using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_boid : MonoBehaviour
{
    public float mass = 1.0f;
    public float maxSpeed = 20.0f;
    public float maxForce = 10.0f;
    public float damping = 0.01f;

    public Vector3 velocity;
    public Vector3 acceleration;

    public float noiseFrequency = 0.3f;
    public float noiseAmplitude = 80.0f;
    public float noiseRadius = 10.0f;
    public float noiseDistance = 5.0f;
    public bool useHorizontalNoise = true;

    private float noiseTheta = 0.0f;

    void Update()
    {
        CalculateSteering();
        UpdatePositionAndRotation();
    }

    void CalculateSteering()
    {
        Vector3 noiseForce = CalculateNoiseWander();
        ApplyForce(noiseForce);
    }

    Vector3 CalculateNoiseWander()
    {
        // Generate a random angle based on Perlin noise
        float noiseValue = Mathf.PerlinNoise(noiseTheta, 1.0f) * 2.0f - 1.0f;
        float angle = noiseValue * noiseAmplitude * Mathf.Deg2Rad;

        // Calculate noise direction based on the selected noise type
        Vector3 noiseDirection;
        if (useHorizontalNoise)
        {
            noiseDirection = new Vector3(Mathf.Sin(angle), 0.0f, Mathf.Cos(angle));
        }
        else
        {
            noiseDirection = new Vector3(0.0f, Mathf.Sin(angle), Mathf.Cos(angle));
        }

        // Calculate the target position on the XZ plane
        Vector3 targetPosition = transform.position + noiseDirection * noiseRadius;
        targetPosition.y = 0.0f; // Project the target position onto the XZ plane

        // Calculate the steering force to seek the target position
        Vector3 steeringForce = SeekForce(targetPosition);

        // Update noise theta for continuous noise variation
        noiseTheta += noiseFrequency * Time.deltaTime * Mathf.PI * 2.0f;

        return steeringForce;
    }

    public Vector3 SeekForce(Vector3 target)
    {
        // Calculate desired velocity towards the target
        Vector3 desiredVelocity = (target - transform.position).normalized * maxSpeed;

        // Calculate steering force needed to achieve the desired velocity
        Vector3 steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

        return steeringForce;
    }

    public void ApplyForce(Vector3 force)
    {
        // Apply the given force to the acceleration, considering the mass of the boid
        acceleration += force / mass;
        acceleration = Vector3.ClampMagnitude(acceleration, maxForce);
    }

    void UpdatePositionAndRotation()
    {
        // Update velocity based on acceleration and apply damping
        velocity += acceleration * Time.deltaTime;
        velocity *= Mathf.Clamp01(1.0f - damping * Time.deltaTime);

        // Clamp the magnitude of velocity to the maximum speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Update position based on velocity
        transform.position += velocity * Time.deltaTime;

        // Update rotation to face the direction of movement
        if (velocity.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(velocity.normalized);
        }

        // Reset acceleration
        acceleration = Vector3.zero;
    }

    public void StopMoving()
    {
        // Stop the boid's movement by resetting velocity and acceleration
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }
}
