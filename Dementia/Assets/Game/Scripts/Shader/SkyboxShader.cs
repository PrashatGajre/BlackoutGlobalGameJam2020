using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SkyboxShader : MonoBehaviour
{
    [SerializeField] Color startColorTop = Color.white;
    [SerializeField] Color startColorBottom = Color.black;

    [SerializeField] Color alarmColorTop = Color.blue;
    [SerializeField] Color alarmColorBottom = Color.magenta;

    [SerializeField] float startIntensity = 1.0f;
    [SerializeField] float startExponent = 1.0f;

    [SerializeField] float alarmIntensity = 1.0f;
    [SerializeField] float alarmExponent = 1.0f;

    void Start()
    {
        RenderSettings.skybox.SetColor("_Color1", startColorBottom);
        RenderSettings.skybox.SetColor("_Color2", startColorTop);
    }

    public void Ambient()
    {
        RenderSettings.skybox.SetColor("_Color1", startColorBottom);
        RenderSettings.skybox.SetColor("_Color2", startColorTop);
        RenderSettings.skybox.SetFloat("Intensity", startIntensity);
        RenderSettings.skybox.SetFloat("Exponent", startExponent);
    }

    public void Alarm()
    {
        RenderSettings.skybox.SetColor("_Color1", alarmColorBottom);
        RenderSettings.skybox.SetColor("_Color2", alarmColorTop);
        RenderSettings.skybox.SetFloat("Intensity", alarmIntensity);
        RenderSettings.skybox.SetFloat("Exponent", alarmExponent);
    }

}
