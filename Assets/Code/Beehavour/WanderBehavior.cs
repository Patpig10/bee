using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : MonoBehaviour
{
    public float wanderRadius = 5.0f;
    public float wanderDistance = 10.0f;
    public float wanderJitter = 1.0f;
    public float maxWanderHeight = 5.0f; // Maximum height above initial position
    public float minWanderHeight = 0.0f; // Minimum height above initial position

    public Boid boid; // Reference to the Boid component
    private Vector3 initialPosition;
    private Vector3 wanderTarget;

    void Start()
    {
        boid = GetComponent<Boid>(); // Get the Boid component attached to this GameObject
        initialPosition = transform.position;
        wanderTarget = GetRandomPointInSphere();
    }

    void Update()
    {
        // Adjust the wander target gradually
        wanderTarget += Random.insideUnitSphere * wanderJitter;
        wanderTarget = Vector3.ClampMagnitude(wanderTarget - transform.position, wanderRadius) + transform.position;

        // Calculate desired velocity towards the wander target
        Vector3 desiredVelocity = (wanderTarget - transform.position).normalized * boid.maxSpeed;

        // Calculate steering force
        Vector3 steeringForce = desiredVelocity - boid.velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, boid.maxForce);

        // Apply the steering force
        boid.ApplyForce(steeringForce);
    }

    Vector3 GetRandomPointInSphere()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderDistance;
        randomDirection += initialPosition;
        randomDirection.y = Mathf.Clamp(randomDirection.y, minWanderHeight, maxWanderHeight); // Clamp height within range
        return randomDirection;
    }
}
