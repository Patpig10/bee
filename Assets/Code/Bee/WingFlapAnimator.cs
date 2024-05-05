using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingFlapAnimator : MonoBehaviour
{
    private float initialRotation;
    public float flapSpeed;
    private float flapAmplitude;
    private float phaseOffset;

    void Start()
    {
        initialRotation = transform.localRotation.eulerAngles.z;
    }

    public void Initialize(bool isLeftWing, float flapSpeed)
    {
        this.flapSpeed = flapSpeed;

        // Set flap amplitude based on wing type (left or right)
        flapAmplitude = isLeftWing ? 30.0f : -30.0f; // Left wing flaps upward, right wing flaps downward
        phaseOffset = Random.Range(0.0f, Mathf.PI); // Random phase offset to add variation
    }

    void Update()
    {
        // Calculate wing flap angle using a sine wave with phase offset
        float time = Time.time * flapSpeed;
        float flapAngle = Mathf.Sin(time + phaseOffset) * flapAmplitude;

        // Apply rotation to the wing around its local z-axis
        transform.localRotation = Quaternion.Euler(0, 0, initialRotation + flapAngle);
    }
}