using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private bool occupied = false;

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool value)
    {
        occupied = value;
    }

    void OnTriggerEnter(Collider other)
    {
        // Detect if a bee enters the flower's trigger area
        if (other.CompareTag("wanderbee"))
        {
            if (!occupied)
            {
                // Occupy the flower
                occupied = true;
            }
        }
    }
}
