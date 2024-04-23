using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStateMachine : MonoBehaviour
{
    public enum BeeState
    {
        Wandering,
        SeekingFlower
    }

    public BeeState currentState = BeeState.Wandering;
    public FlowerSeekBehavior flowerSeekBehavior;
    public WanderBehavior wanderBehavior;

    public float switchInterval = 13f;       // Interval in seconds for switching to seeking flower
    public string targetFlowerTag = "Flower"; // Tag of the flower to seek

    private float lastSwitchTime;

    void Start()
    {
        // Get references to behaviors
        wanderBehavior = GetComponent<WanderBehavior>();
        flowerSeekBehavior = GetComponent<FlowerSeekBehavior>();

        // Enable the initial behavior (wandering)
        EnableBehavior(wanderBehavior);
        DisableBehavior(flowerSeekBehavior);

        // Initialize last switch time
        lastSwitchTime = Time.time;
    }

    void Update()
    {
        switch (currentState)
        {
            case BeeState.Wandering:
                if (ShouldSwitchToSeekingFlower())
                {
                    TransitionToSeekingFlower();
                }
                break;

            case BeeState.SeekingFlower:
                if (flowerSeekBehavior.isSitting)
                {
                    // Bee is sitting on the flower, wait for sit duration
                    flowerSeekBehavior.SitOnFlowerTimer();

                    if (!flowerSeekBehavior.isSitting)
                    {
                        // Finished sitting on the flower, transition back to wandering
                        TransitionToWandering();
                    }
                }
                break;
        }
    }

    bool ShouldSwitchToSeekingFlower()
    {
        // Check if enough time has passed to switch to seeking flower
        if (Time.time - lastSwitchTime >= switchInterval)
        {
            // Check if there's a nearby flower with the target tag
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, flowerSeekBehavior.detectRadius);
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag(targetFlowerTag))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void TransitionToSeekingFlower()
    {
        currentState = BeeState.SeekingFlower;
        EnableBehavior(flowerSeekBehavior);
        DisableBehavior(wanderBehavior);

        // Update last switch time
        lastSwitchTime = Time.time;
    }

    void TransitionToWandering()
    {
        currentState = BeeState.Wandering;
        EnableBehavior(wanderBehavior);
        DisableBehavior(flowerSeekBehavior);

        // Update last switch time
        lastSwitchTime = Time.time;
    }

    void EnableBehavior(MonoBehaviour behavior)
    {
        if (behavior != null)
        {
            behavior.enabled = true;
        }
    }

    void DisableBehavior(MonoBehaviour behavior)
    {
        if (behavior != null)
        {
            behavior.enabled = false;
        }
    }
}