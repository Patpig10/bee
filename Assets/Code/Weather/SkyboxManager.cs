using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public void ChangeSkybox(string skyboxName)
    {
        Material skybox = Resources.Load<Material>("Skyboxes/" + skyboxName);
        if (skybox != null)
        {
            RenderSettings.skybox = skybox;
        }
        else
        {
            Debug.LogError("Skybox not found: " + skyboxName);
        }
    }
}
