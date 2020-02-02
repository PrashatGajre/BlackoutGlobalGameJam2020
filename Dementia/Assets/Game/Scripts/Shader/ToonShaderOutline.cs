using UnityEngine;
using System.Collections.Generic;
public class ToonShaderOutline : MonoBehaviour
{    
    public Material toonMaterial;
    public Material toonMaterialHighlight;
    public Material toonMaterialSnapped;

    Material[] materials;
    MeshRenderer[] meshRenderers = new MeshRenderer[1];
    private void Start()
    {
        materials = new Material[1];
        materials[0] = toonMaterial;
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
    }

    public void MouseDown()
    {
        materials = new Material[2];
        materials[0] = toonMaterial;
        materials[1] = toonMaterialHighlight;
        foreach (MeshRenderer mr in meshRenderers)
        {
            if(mr.GetComponent<SnapPoint>() == null)
            mr.materials = materials;
        }
    }

    public void MouseUp()
    {
        materials = new Material[1];
        materials[0] = toonMaterial;
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.materials = materials;
        }
    }

    public void SnappedMaterial()
    {
        materials = new Material[2];
        materials[0] = toonMaterial;
        materials[1] = toonMaterialSnapped;
        foreach (MeshRenderer mr in meshRenderers)
        {
            if (mr.GetComponent<SnapPoint>() == null)
                mr.materials = materials;
        }
    }
}