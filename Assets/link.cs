using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link : MonoBehaviour
{
    public Transform parentTransform; // Reference to the parent's Transform

    void Start()
    {
        // Get the parent's Transform component
        parentTransform = transform.parent;

        if (parentTransform == null)
        {
            Debug.LogError("Parent transform is missing. Make sure this script is attached to a child GameObject with a parent.");
            enabled = false; // Disable the script if the parent is missing
        }
    }

    void LateUpdate()
    {
        // Sync the child's rotation with the parent's rotation
        if (parentTransform != null)
        {
            transform.rotation = parentTransform.rotation;
        }
    }
}
