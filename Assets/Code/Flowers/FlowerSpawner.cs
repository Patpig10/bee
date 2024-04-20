using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject flowerPrefab;
    public int numberOfFlowers = 10;
    public float spawnRadius = 10f;
    public float minDistance = 2f;
    public Color[] petalColors;

    private List<Vector3> flowerPositions = new List<Vector3>();

    void Start()
    {
        SpawnFlowers();
    }

    void SpawnFlowers()
    {
        for (int i = 0; i < numberOfFlowers; i++)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();

            GameObject newFlower = Instantiate(flowerPrefab, randomPosition, Quaternion.identity);

            MeshRenderer[] petalRenderers = newFlower.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer renderer in petalRenderers)
            {
                if (renderer.CompareTag("Petal"))
                {
                    int randomColorIndex = Random.Range(0, petalColors.Length);
                    renderer.material.color = petalColors[randomColorIndex];
                }
            }

            flowerPositions.Add(randomPosition); // Store the position of the new flower
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;

        // Check minimum distance from other flowers
        while (!CheckMinDistance(randomPosition))
        {
            randomCircle = Random.insideUnitCircle * spawnRadius;
            randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;
        }

        return randomPosition;
    }

    bool CheckMinDistance(Vector3 position)
    {
        foreach (Vector3 flowerPos in flowerPositions)
        {
            if (Vector3.Distance(position, flowerPos) < minDistance)
            {
                return false;
            }
        }

        return true;
    }
}
