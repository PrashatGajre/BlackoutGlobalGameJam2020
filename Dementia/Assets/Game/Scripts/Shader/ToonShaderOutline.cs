using UnityEngine;
using System.Collections.Generic;
public class ToonShaderOutline : MonoBehaviour
{    
    public Material toonMaterial;
    public Material toonMaterialHighlight;

    Material[] materials;
    MeshRenderer[] meshRenderers = new MeshRenderer[1];
    private void Start()
    {
        materials = new Material[1];
        materials[0] = toonMaterial;
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
    }

    private void OnMouseDown()
    {
        materials = new Material[2];
        materials[0] = toonMaterial;
        materials[1] = toonMaterialHighlight;
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.materials = materials;
        }
    }

    private void OnMouseUp()
    {
        materials = new Material[1];
        materials[0] = toonMaterial;
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.materials = materials;
        }
    }
}