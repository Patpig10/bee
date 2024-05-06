using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCollisionBehavior : MonoBehaviour
{
    public float disableDuration = 6.0f; // Duration to disable flying upon rain collision

    private Boid boid;
    private Vector3 initialPosition;
    public bool isFlyingEnabled = true;

    private void Start()
    {
        boid = GetComponent<Boid>();
        initialPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("rain") && isFlyingEnabled)
        {
            DisableFlying();
            Invoke("EnableFlying", disableDuration);
        }
    }

    private void DisableFlying()
    {
        if (isFlyingEnabled)
        {
            boid.velocity = new Vector3(boid.velocity.x, 0.0f, boid.velocity.z); // Zero out y-component of velocity
            isFlyingEnabled = false;
        }
    }

    private void EnableFlying()
    {
        isFlyingEnabled = true;
    }

    private void Update()
    {
        if (!isFlyingEnabled)
        {
            // Smoothly interpolate y-position back to initial position
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime);
        }
    }
}
