using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightBehavior : MonoBehaviour
{
    public float flightSpeed = 10f;
    public KeyCode landingKey = KeyCode.Space;

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
        // Simulate flight behavior (move forward)
        transform.Translate(Vector3.forward * flightSpeed * Time.deltaTime);

        // Additional flight logic (e.g., tilt based on input)
    }

    private void TransitionToGrounded()
    {
        GetComponent<GroundedBehavior>().enabled = true; // Enable GroundedBehavior
        enabled = false; // Disable FlightBehavior
    }

}
