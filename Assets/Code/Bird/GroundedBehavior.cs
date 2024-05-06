using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedBehavior : MonoBehaviour
{
    public float hopForce = 5f;
    public float maxHopHeight = 2f;
    public KeyCode takeoffKey = KeyCode.Space;

    private bool isGrounded = true;

    private void Update()
    {
        if (isGrounded)
        {
            // Perform grounded hopping behavior
            Hop();

            // Check for takeoff transition
            if (Input.GetKeyDown(takeoffKey))
            {
                TransitionToFlight();
            }
        }
    }

    private void Hop()
    {
        // Simulate hopping behavior (move upwards)
        Vector3 hopVelocity = Vector3.up * hopForce * Time.deltaTime;
        transform.position += hopVelocity;

        // Check if the bird reaches maximum hop height
        if (transform.position.y >= maxHopHeight)
        {
            // Limit the height to maxHopHeight
            transform.position = new Vector3(transform.position.x, maxHopHeight, transform.position.z);
            // Transition to flight mode
            TransitionToFlight();
        }
    }

    private void TransitionToFlight()
    {
        isGrounded = false;
        // Additional transition logic (e.g., disable grounded visuals)
        FlightBehavior flightBehavior = GetComponent<FlightBehavior>();
        if (flightBehavior != null)
        {
            flightBehavior.enabled = true; // Enable FlightBehavior
        }
        enabled = false; // Disable GroundedBehavior
    }
}
