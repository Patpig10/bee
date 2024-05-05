using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStateMachine : MonoBehaviour
{
    public WeatherController weatherController;
    public enum BeeState
    {
        Wandering,
        FlowerSeeking,
        GoToTree
    }

    public BeeState currentState = BeeState.Wandering;

    private void Start()
    {
        // Start the default state (Wandering)
        StartCoroutine(TransitionStates());
    }

    private IEnumerator TransitionStates()
    {
        while (true)
        {
            switch (currentState)
            {
                case BeeState.Wandering:
                    // After 15 seconds, switch to FlowerSeeking
                    yield return new WaitForSeconds(30f);
                    currentState = BeeState.FlowerSeeking;
                    break;

                case BeeState.FlowerSeeking:
                    // Check if it's raining, if so switch to GoToTree
                    if (weatherController.isRaining)
                    {
                        currentState = BeeState.GoToTree;
                    }
                    else
                    {
                        // Otherwise, return to Wandering
                        currentState = BeeState.Wandering;
                    }
                    break;

                case BeeState.GoToTree:
                    // Wait for rain to stop, then return to Wandering
                    while (weatherController.isRaining)
                    {
                        yield return null;
                    }
                    currentState = BeeState.Wandering;
                    break;
            }

            // Activate behaviors based on the current state
            ActivateBehaviors();

            yield return null;
        }
    }

    private void ActivateBehaviors()
    {
        switch (currentState)
        {
            case BeeState.Wandering:
                // Enable WanderBehavior and disable others
                GetComponent<WanderBehavior>().enabled = true;
                GetComponent<GoToTreeBehavior>().enabled = false;
                GetComponent<GoToTreeBehavior>().enabled = false;
                break;

            case BeeState.FlowerSeeking:
                // Enable GoToFlowerBehavior and disable others
                GetComponent<WanderBehavior>().enabled = false;
                GetComponent<GoToTreeBehavior>().enabled = true;
                GetComponent<GoToTreeBehavior>().enabled = false;
                break;

            case BeeState.GoToTree:
                // Enable GoToTreeBehavior and disable others
                GetComponent<WanderBehavior>().enabled = false;
                GetComponent<GoToTreeBehavior>().enabled = false;
                GetComponent<GoToTreeBehavior>().enabled = true;
                break;
        }
    }

  
}