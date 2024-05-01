using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RaindropSpawner : MonoBehaviour
{
  
    public GameObject raindropPrefab;   // Prefab for raindrops
    public float raindropSpeedMin = 5f;  // Minimum speed of raindrops
    public float raindropSpeedMax = 10f; // Maximum speed of raindrops

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning && raindropPrefab != null)
        {
            isSpawning = true;
            InvokeRepeating("SpawnRaindrop", 0f, 0.1f); // Invoke SpawnRaindrop method continuously with interval
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnRaindrop");
    }

    private void SpawnRaindrop()
    {
        if (raindropPrefab == null)
            return;

        // Calculate random position within camera view for raindrop spawn
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        Vector3 spawnPosition = new Vector3(
            Random.Range(-camWidth, camWidth),
            camHeight,
            0f
        );

        // Instantiate raindrop prefab at the calculated position
        GameObject raindrop = Instantiate(raindropPrefab, spawnPosition, Quaternion.identity);

        // Set random speed for raindrop
        Rigidbody rb = raindrop.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float raindropSpeed = Random.Range(raindropSpeedMin, raindropSpeedMax);
            rb.velocity = Vector3.down * raindropSpeed;
        }
    }
}

