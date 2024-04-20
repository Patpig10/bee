using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBeeCreature : MonoBehaviour
{
    public GameObject bodyPrefab;         // Prefab for the creature's body
    public GameObject leftWingPrefab;     // Prefab for the left wing
    public GameObject rightWingPrefab;    // Prefab for the right wing

    public float bodySize = 1.0f;         // Size of the body
    public float wingSize = 1.0f;         // Size of the wings
    public float wingOffset = 0.3f;       // Horizontal offset for positioning wings
    public float wingHeightOffset = 0.5f; // Vertical offset for positioning wings above the body
    public float wingDepthOffset = 0.3f;  // Offset along z-axis for positioning wings behind the body
    public float flapSpeed = 5.0f;        // Speed of the wing flapping

    void Start()
    {
        CreateBeeCreature1();
    }

    public void CreateBeeCreature1()
    {
        // Create the body
        GameObject body = Instantiate(bodyPrefab, transform);
        body.transform.localScale = new Vector3(bodySize, bodySize, bodySize);

        // Calculate the positions for the left and right wings (on top of the body)
        Vector3 leftWingPosition = new Vector3(-wingOffset * bodySize, wingHeightOffset * bodySize, -wingDepthOffset * bodySize);
        Vector3 rightWingPosition = new Vector3(wingOffset * bodySize, wingHeightOffset * bodySize, -wingDepthOffset * bodySize);

        // Create the left wing
        GameObject leftWing = Instantiate(leftWingPrefab, body.transform);
        leftWing.transform.localPosition = leftWingPosition;
        leftWing.transform.localScale = new Vector3(wingSize, wingSize, wingSize);
        leftWing.AddComponent<WingFlapAnimator>().Initialize(true, flapSpeed);  // Assign flap speed and mark as left wing

        // Create the right wing
        GameObject rightWing = Instantiate(rightWingPrefab, body.transform);
        rightWing.transform.localPosition = rightWingPosition;
        rightWing.transform.localScale = new Vector3(wingSize, wingSize, wingSize);
        rightWing.AddComponent<WingFlapAnimator>().Initialize(false, flapSpeed);  // Assign flap speed and mark as right wing

        // Adjust wing position by pushing them further back along the z-axis
        leftWing.transform.localPosition += new Vector3(0, 0, -wingDepthOffset * bodySize);
        rightWing.transform.localPosition += new Vector3(0, 0, -wingDepthOffset * bodySize);
    }
}
