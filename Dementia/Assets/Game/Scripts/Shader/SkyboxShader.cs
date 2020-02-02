using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SkyboxShader : MonoBehaviour
{
    [SerializeField]Color startColorTop = Color.white;
    [SerializeField]Color startColorBottom = Color.black;

    [SerializeField] Color alarmColorTop = Color.blue;
    [SerializeField] Color alarmColorBottom = Color.magenta;

    void Start()
    {
        RenderSettings.skybox.SetColor("_Color1", startColorBottom);
        RenderSettings.skybox.SetColor("_Color2", startColorTop);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            RenderSettings.skybox.SetColor("_Color1", startColorBottom);
            RenderSettings.skybox.SetColor("_Color2", startColorTop);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            RenderSettings.skybox.SetColor("_Color1", alarmColorBottom);
            RenderSettings.skybox.SetColor("_Color2", alarmColorTop);
        }
    }

}
