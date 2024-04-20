using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    void Start()
    {
        Boid boid = GetComponent<Boid>();
        if (boid != null)
        {
            // Attach WanderBehavior to enable wandering for this bee
            WanderBehavior wanderBehavior = gameObject.AddComponent<WanderBehavior>();
            wanderBehavior.wanderRadius = 5.0f;
            wanderBehavior.wanderDistance = 10.0f;
            wanderBehavior.wanderJitter = 1.0f;
            wanderBehavior.maxWanderHeight = 5.0f;
            wanderBehavior.minWanderHeight = 0.0f;
        }
    }
}
