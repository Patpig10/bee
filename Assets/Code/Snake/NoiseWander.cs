using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseWander : MonoBehaviour
{
    public float noiseFrequency = 0.3f;
    public float noiseAmplitude = 80.0f;
    public float noiseRadius = 10.0f;
    public bool useHorizontalNoise = true;

    private float noiseTheta = 0.0f;

    public Vector3 CalculateNoiseWander()
    {
        float noiseValue = Mathf.PerlinNoise(noiseTheta, 1.0f) * 2.0f - 1.0f;
        float angle = noiseValue * noiseAmplitude * Mathf.Deg2Rad;

        Vector3 noiseDirection;
        if (useHorizontalNoise)
        {
            noiseDirection = new Vector3(Mathf.Sin(angle), 0.0f, Mathf.Cos(angle));
        }
        else
        {
            noiseDirection = new Vector3(0.0f, Mathf.Sin(angle), Mathf.Cos(angle));
        }

        Vector3 targetPosition = transform.position + noiseDirection * noiseRadius;

        noiseTheta += noiseFrequency * Time.deltaTime * Mathf.PI * 2.0f;

        return targetPosition;
    }
}
