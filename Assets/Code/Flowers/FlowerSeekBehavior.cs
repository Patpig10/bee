using System.Collections;
using UnityEngine;

public class FlowerSeekBehavior : MonoBehaviour
{
    public float detectRadius = 5.0f;       // Radius to detect flowers
    public LayerMask flowerLayer;           // Layer mask for flowers
    public float sitDistance = 0.5f;        // Distance threshold to consider bee sitting on the flower
    public float sitDuration = 4.0f;        // Duration to sit on the flower in seconds
    public float seekCooldownDuration = 100.0f;  // Cooldown duration after sitting before resuming seeking

    private Boid boid;
    private Flower targetFlower;
    private bool isSitting = false;
    private bool isCooldown = false;

    void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Update()
    {
        if (!isSitting && !isCooldown)
        {
            LookForFlower();
        }
        else if (isSitting)
        {
            if (targetFlower != null)
            {
                // Check if bee is within sit distance of the flower
                float distanceToFlower = Vector3.Distance(transform.position, targetFlower.transform.position);
                if (distanceToFlower <= sitDistance)
                {
                    // Bee is sitting on the flower
                    Debug.Log("Bee is sitting on the flower: " + targetFlower.name);
                    targetFlower.SetOccupied(true);

                    // Wait for sitDuration
                    StartCoroutine(SitOnFlowerTimer());
                }
                else
                {
                    // Move towards the flower
                    boid.Arrive(targetFlower.transform.position);
                }
            }
        }
    }

    void LookForFlower()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius, flowerLayer);

        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                Flower flower = collider.GetComponent<Flower>();

                if (flower != null && !flower.IsOccupied())
                {
                    Debug.Log("Bee found an unoccupied flower!");

                    // Move to the flower and sit on it
                    targetFlower = flower;
                    isSitting = true;
                    break; // Stop checking for other flowers
                }
            }
        }
    }

    IEnumerator SitOnFlowerTimer()
    {
        yield return new WaitForSeconds(sitDuration);

        // Release the flower
        if (targetFlower != null)
        {
            Debug.Log("Bee finished sitting on the flower: " + targetFlower.name);
            targetFlower.SetOccupied(false);
            targetFlower = null;
            isSitting = false;

            // Start the cooldown timer
            StartCoroutine(SeekCooldownTimer());
        }
    }

    IEnumerator SeekCooldownTimer()
    {
        isCooldown = true;
        yield return new WaitForSeconds(seekCooldownDuration);
        isCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        // Draw the sit distance radius in the scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sitDistance);
    }
}
