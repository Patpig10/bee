using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropSpawner : MonoBehaviour
{
    public Vector3 spawnCenter = Vector3.zero; // Center of the spawn area
    public Vector3 spawnSize = new Vector3(10f, 10f, 10f); // Size of the spawn area (width, height, depth)
    public float minFallSpeed = 3f; // Minimum falling speed of cubes
    public float maxFallSpeed = 8f; // Maximum falling speed of cubes
    public float cubeLifetime = 10f; // Lifetime of cubes before they disappear
    public float minScaleFactor = 0.8f; // Minimum scale factor for cubes
    public float maxScaleFactor = 1.2f; // Maximum scale factor for cubes
    public Material rainMaterial; // Material to apply to the cubes
    public float spawnInterval = 0.5f; // Interval between cube spawns

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            InvokeRepeating("SpawnCube", 0f, spawnInterval); // Invoke SpawnCube method continuously with interval
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnCube");
    }

    private void SpawnCube()
    {
        // Calculate random position within spawn area for cube spawn
        Vector3 spawnPosition = spawnCenter + new Vector3(
            Random.Range(-spawnSize.x / 2f, spawnSize.x / 2f),
            spawnSize.y, // Spawn cubes from the top of the spawn area
            Random.Range(-spawnSize.z / 2f, spawnSize.z / 2f)
        );

        // Instantiate a cube at the calculated position
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = spawnPosition;

        // Randomize cube scale
        float scaleFactor = Random.Range(minScaleFactor, maxScaleFactor);
        cube.transform.localScale *= scaleFactor;

        // Apply rain material to the cube
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer != null && rainMaterial != null)
        {
            cubeRenderer.material = rainMaterial;
        }

        // Set the "rain" tag to the cube
        cube.tag = "rain";

        // Set random falling speed for the cube
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.velocity = Vector3.down * Random.Range(minFallSpeed, maxFallSpeed);

        // Destroy the cube after the specified lifetime
        Destroy(cube, cubeLifetime);
    }

    private void OnDrawGizmos()
    {
        // Draw spawn area wireframe gizmo in editor
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(spawnCenter, spawnSize);
    }

    // Method to adjust the spawn area size
    public void AdjustSpawnSize(Vector3 newSize)
    {
        spawnSize = newSize;
    }

    // Method to adjust the spawn interval
    public void AdjustSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
        if (isSpawning)
        {
            StopSpawning();
            StartSpawning();
        }
    }
}
