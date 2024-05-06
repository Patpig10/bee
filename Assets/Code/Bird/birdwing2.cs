using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdwing2 : MonoBehaviour
{

    public Transform rightWing; // Reference to the right wing transform
    public float flapSpeed = 5.0f; // Speed of the wing flap
    public float flapAmplitude = 30.0f; // Amplitude of the wing flap

    private Quaternion initialRotation; // Initial rotation of the right wing

    void Start()
    {
        // Store the initial rotation of the right wing
        initialRotation = rightWing.localRotation;
    }

    void Update()
    {
        // Calculate wing rotation angles based on sine wave motion
        float wingRotation = Mathf.Sin(Time.time * flapSpeed) * flapAmplitude;

        // Apply rotation to the right wing around its local z-axis (right side)
        rightWing.localRotation = initialRotation * Quaternion.Euler(0f, 0f, wingRotation);
    }

}
