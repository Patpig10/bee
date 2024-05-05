using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public SkyboxManager skyboxManager;

    public RaindropSpawner raindropSpawner;

    private Weather currentWeather;

    public bool isRaining = false;
    public bool isWindy = false;
    public bool isSunny = false;
    void Start()
    {
        // Initialize with a default weather (e.g., sunny)
        ChangeWeather(new Sunny(skyboxManager));
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeToRainy();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeToSunny();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeToWindy();
        }
    }
    public void ChangeWeather(Weather newWeather)
    {
        if (currentWeather != null)
        {
            currentWeather.Stop();
        }

        currentWeather = newWeather;
        currentWeather.Start();
    }

    public void ChangeToRainy()
    {
        ChangeWeather(new Rainy(skyboxManager));
        raindropSpawner.StartSpawning();
        isRaining = true;
        isSunny = false;
        isWindy = false;
    }

    public void ChangeToSunny()
    {
        ChangeWeather(new Sunny(skyboxManager));
        raindropSpawner.StopSpawning();
        isRaining = false;
        isSunny = true;
        isWindy = false;
    }

    public void ChangeToWindy()
    {
        ChangeWeather(new Windy(skyboxManager));
        raindropSpawner.StopSpawning();
        isRaining = false;
        isSunny = false;
        isWindy = true;

    }
}
