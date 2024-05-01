using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public SkyboxManager skyboxManager;

    private Weather currentWeather;

    void Start()
    {
        // Initialize with a default weather (e.g., sunny)
        ChangeWeather(new Sunny(skyboxManager));
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
    }

    public void ChangeToSunny()
    {
        ChangeWeather(new Sunny(skyboxManager));
    }

    public void ChangeToWindy()
    {
        ChangeWeather(new Windy(skyboxManager));
    }
}
