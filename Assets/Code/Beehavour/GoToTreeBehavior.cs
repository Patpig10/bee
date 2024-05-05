using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GoToTreeBehavior : MonoBehaviour
{
    public string treeTag = "tree"; // Tag for the tree objects
    public float movementSpeed = 2.0f; // Speed of movement towards the tree
    public float arriveThreshold = 0.5f; // Distance threshold to consider arrival at the tree
    public float hoverRadius = 1.0f; // Radius for hovering around the tree
    public float hoverSpeed = 1.0f; // Speed of hovering around the tree

    private GameObject targetTree;
    public bool isMoving = false;

    private void Update()
    {
        if (!isMoving)
        {
            // Find the nearest tree and initiate movement towards it
            FindNearestTree();
            if (targetTree != null)
            {
                StartCoroutine(MoveToTree(targetTree.transform.position));
            }
        }

        if (isMoving && targetTree != null)
        {
            // Check distance to the tree
            float distanceToTree = Vector3.Distance(transform.position, targetTree.transform.position);

            if (distanceToTree <= arriveThreshold)
            {
                // Arrived at the tree, start hovering around it
                StartCoroutine(HoverAroundTree(targetTree.transform.position));
            }
        }

        // Disengage from the tree when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isMoving)
        {
            StopMoving();
        }
    }

    private void FindNearestTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag(treeTag);

        float closestDistance = Mathf.Infinity;
        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetTree = tree;
            }
        }
    }

    private IEnumerator MoveToTree(Vector3 targetPosition)
    {
      //  isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > arriveThreshold)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * movementSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator HoverAroundTree(Vector3 treePosition)
    {
        while (true)
        {
            // Circle around the tree
            Vector3 circlePosition = treePosition + (Quaternion.Euler(0, Time.time * hoverSpeed, 0) * Vector3.forward) * hoverRadius;

            // Restrict the y position within the range 24 to 32
            circlePosition.y = Mathf.Clamp(circlePosition.y, 24f, 32f);

            transform.position = circlePosition;

            yield return null;
        }
    }

    private void StopMoving()
    {
        StopAllCoroutines();
        isMoving = false;
    }
}
