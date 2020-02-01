using UnityEngine;
[ExecuteInEditMode, ImageEffectAllowedInSceneView, RequireComponent(typeof(Camera))]
public class DitherEffect : MonoBehaviour
{
    [Tooltip("Assign a material with the Dither Shader.")]
    public Material ditherMat;
    [Tooltip("Strength of the Dither effect 0 gives a coarser look, 1 gives finer grain.")]
    [Range(0.0f, 1.0f)]
    public float ditherStrength = 0.15f;
    [Tooltip("Color depth at 1 gives faint color, 32 gives a richer color.")]
    [Range(1, 32)]
    public int colourDepth = 1;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        ditherMat.SetFloat("_DitherStrength", ditherStrength);
        ditherMat.SetInt("_ColourDepth", colourDepth);
        Graphics.Blit(src, dest, ditherMat);
    }
}