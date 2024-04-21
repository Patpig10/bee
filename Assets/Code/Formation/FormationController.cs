using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    public GameObject leader;               // Reference to the leader bee
    public GameObject followerPrefab;       // Prefab for the follower bee
    public int numRows = 3;                 // Number of rows of followers
    public int numFollowersPerRow = 5;      // Number of followers in each row
    public float rowSpacing = 1.5f;         // Spacing between rows of followers
    public float followerSpacing = 0.5f;    // Spacing between followers in a row
    public Vector3 offsetPerRow = new Vector3(0f, 0f, -1f); // Offset for each row (behind the leader)

    private List<GameObject> followers = new List<GameObject>();

    void Start()
    {
        // Instantiate the leader bee
        if (leader != null)
        {
            Instantiate(leader, transform.position, Quaternion.identity);
        }

        // Calculate the formation positions for followers
        CreateFormation();
    }

    void CreateFormation()
    {
        Vector3 leaderPosition = leader.transform.position;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numFollowersPerRow; col++)
            {
                // Calculate position for each follower
                float xOffset = col * followerSpacing;
                float zOffset = -row * rowSpacing; // Negative zOffset to position followers behind the leader
                Vector3 followerPosition = leaderPosition + offsetPerRow * zOffset + new Vector3(xOffset, 0f, 0f);

                // Instantiate the follower bee at the calculated position
                GameObject newFollower = Instantiate(followerPrefab, followerPosition, Quaternion.identity);
                followers.Add(newFollower);
            }
        }
    }

    void Update()
    {
        // Update follower positions to follow the leader
        if (leader != null)
        {
            UpdateFormation();
        }
    }

    void UpdateFormation()
    {
        Vector3 leaderPosition = leader.transform.position;

        for (int i = 0; i < followers.Count; i++)
        {
            float xOffset = (i % numFollowersPerRow) * followerSpacing;
            int row = i / numFollowersPerRow;
            float zOffset = -row * rowSpacing; // Negative zOffset to position followers behind the leader

            // Calculate target position relative to the leader
            Vector3 targetPosition = leaderPosition + offsetPerRow * zOffset + new Vector3(xOffset, 0f, 0f);

            // Move follower towards the target position
            followers[i].transform.position = Vector3.Lerp(followers[i].transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
