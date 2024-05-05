using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCollisionBehavior : MonoBehaviour
{
    public float disableDuration = 6.0f; // Duration to disable flying upon rain collision
    public float slerpSpeed = 2.0f; // Speed of interpolation

    private Boid boid;
    private bool isFlyingEnabled = true;
    private Vector3 initialPosition;

    void Start()
    {
        boid = GetComponent<Boid>();
        initialPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("rain") && isFlyingEnabled)
        {
            DisableFlying();
            Invoke("EnableFlying", disableDuration);
        }
    }

    void DisableFlying()
    {
        if (isFlyingEnabled)
        {
            // Zero out the y-component of velocity to disable flying
            boid.velocity = new Vector3(boid.velocity.x, 0.0f, boid.velocity.z);
            isFlyingEnabled = false;
        }
    }

    void EnableFlying()
    {
        isFlyingEnabled = true;
    }

    void Update()
    {
        // Smoothly transition y-position back to original height when flying is enabled
        if (isFlyingEnabled)
        {
            transform.position = Vector3.Slerp(transform.position, initialPosition, Time.deltaTime * slerpSpeed);
        }
    }
}
