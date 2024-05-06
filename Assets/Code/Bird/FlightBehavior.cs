using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightBehavior : MonoBehaviour
{
    public float flightSpeed = 10f;
    public float wanderRange = 5f; // Range within which the bird can wander
    public float minAltitude = 1f; // Minimum altitude the bird should maintain
    public float changeDirectionInterval = 3f; // Interval to change flight direction in seconds
    public KeyCode landingKey = KeyCode.Space;

    private Vector3 flightDirection; // Current flight direction
    private float directionChangeTimer; // Timer to track direction change intervals

    private void Start()
    {
        // Initialize flight direction to a random direction
        flightDirection = GetRandomDirection().normalized;

        // Start the direction change timer
        directionChangeTimer = changeDirectionInterval;
    }

    private void Update()
    {
        // Perform flight behavior
        Fly();

        // Check for landing transition
        if (Input.GetKeyDown(landingKey))
        {
            TransitionToGrounded();
        }
    }

    private void Fly()
    {
        // Update position based on flight speed and current flight direction
        transform.Translate(flightDirection * flightSpeed * Time.deltaTime, Space.World);

        // Clamp altitude to stay above the minimum altitude
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Max(currentPosition.y, minAltitude);
        transform.position = currentPosition;

        // Rotate bird to face the direction it's moving in
        if (flightDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(flightDirection, Vector3.up);
        }

        // Update direction change timer
        directionChangeTimer -= Time.deltaTime;

        // Check if it's time to change flight direction
        if (directionChangeTimer <= 0f)
        {
            // Get a new random flight direction
            flightDirection = GetRandomDirection().normalized;

            // Reset the direction change timer
            directionChangeTimer = changeDirectionInterval;
        }
    }

    private Vector3 GetRandomDirection()
    {
        // Generate a random direction within the 2D plane (x, z)
        return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    private void TransitionToGrounded()
    {
        GetComponent<GroundedBehavior>().enabled = true; // Enable GroundedBehavior
        enabled = false; // Disable FlightBehavior
    }
}
