using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamCameraController : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed of camera movement
    public float rotationSpeed = 2f; // Speed of camera rotation
    public GameObject followTarget; // Target to follow when selected

    private bool isFollowingTarget = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleInput();

        if (isFollowingTarget && followTarget != null)
        {
            // Update camera position to follow the target
            transform.position = followTarget.transform.position;
        }
    }

    void HandleInput()
    {
        // Camera movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.Self);

        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector3.right, -mouseY);

        // Check for creature selection
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Creature creature = hit.collider.GetComponent<Creature>();
                if (creature != null)
                {
                    // Clicked on a creature, start following it
                    StartFollowing(creature.gameObject);
                }
            }
        }

        // Stop following if Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopFollowing();
        }
    }

    void StartFollowing(GameObject target)
    {
        followTarget = target;
        isFollowingTarget = true;
    }

    void StopFollowing()
    {
        followTarget = null;
        isFollowingTarget = false;
    }

}
