using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdWingAnimator : MonoBehaviour
{
    public Transform leftWing; // Reference to the left wing transform
    public float flapSpeed = 5.0f; // Speed of the wing flap

    void Update()
    {
        // Calculate wing rotation angles based on sine wave motion
        float leftWingRotation = Mathf.Sin(Time.time * flapSpeed) * -52.0f;

        // Apply rotation to left and right wings
        leftWing.localRotation = Quaternion.Euler(0f, 0f, leftWingRotation);
    }
}
