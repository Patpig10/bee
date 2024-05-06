using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdWingAnimator : MonoBehaviour
{
    public Transform leftWing; // Reference to the left wing transform
    public Transform rightWing; // Reference to the right wing transform
    public float flapSpeed = 5.0f; // Speed of the wing flap
    public float flapAmplitude = 30.0f; // Amplitude of the wing flap

    private Quaternion leftInitialRotation; // Initial rotation of the left wing
    private Quaternion rightInitialRotation; // Initial rotation of the right wing

    void Start()
    {
        // Store the initial rotations of the wings
        leftInitialRotation = leftWing.localRotation;
        rightInitialRotation = rightWing.localRotation;
    }

    void Update()
    {
        // Calculate wing rotation angles based on sine wave motion
        float wingRotation = Mathf.Sin(Time.time * flapSpeed) * flapAmplitude;

        // Apply rotation to the left wing around its local z-axis
        leftWing.localRotation = leftInitialRotation * Quaternion.Euler(0f, 0f, wingRotation);

        // Apply rotation to the right wing around its local z-axis
        rightWing.localRotation = rightInitialRotation * Quaternion.Euler(0f, 0f, -wingRotation); // Negative rotation for opposite side
    }
}
