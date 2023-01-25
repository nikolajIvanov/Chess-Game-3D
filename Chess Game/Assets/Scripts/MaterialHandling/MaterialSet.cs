using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MaterialIdentifier
{
    Main,
    Color,
    Detail
}

[Serializable]
public struct MaterialSetEntry
{
    public MaterialIdentifier identifier;
    public Material material;
}

[Serializable]
public struct RendererSlot
{
    public Renderer meshRenderer;
    public int materialSlot;
}

[Serializable]
public struct MaterialAssignment
{
    public MaterialIdentifier materialID;
    public RendererSlot[] affectedRenderers;
}

[CreateAssetMenu(fileName = "MaterialSet", menuName = "Scriptable Objects/MaterialSet", order = 1)]
public class MaterialSet : ScriptableObject
{
    // would be nicer to have this as a Dictionary but I don't think you can directly edit these in the editor (for these few entries an array should be fine)
    public MaterialSetEntry[] materials;

    public Material GetMaterial(MaterialIdentifier identifier)
    {
        int index = 0;
        while(index < materials.Length)
        {
            if(materials[index].identifier == identifier)
            {
                // found the material
                return materials[index].material;
            }
            else
            {
                ++index;
            }
        }

        // no material found corresponding to the given identifier
        return null;
    }
}
