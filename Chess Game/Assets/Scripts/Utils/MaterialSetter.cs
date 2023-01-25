using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class MaterialSetter : MonoBehaviour
{
    [SerializeField]
    private MaterialAssignment[] materialAssignments;

    [SerializeField] private MeshRenderer _meshRenderer;
    private MeshRenderer meshRenderer
    {
        get
        {
            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();
            return _meshRenderer;
        }
    }

    public void SetSingleMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    public void SetMaterialSet(MaterialSet materialSet)
    {
        foreach(MaterialAssignment materialAssignment in materialAssignments)
        {
            Material targetMaterial = materialSet.GetMaterial(materialAssignment.materialID);

            foreach(RendererSlot rendererSlot in materialAssignment.affectedRenderers)
            {
                // materials array needs to be set as a whole, not possible to just change single elements directly
                Material[] materialsTemp = rendererSlot.meshRenderer.sharedMaterials;
                materialsTemp[rendererSlot.materialSlot] = targetMaterial;
                rendererSlot.meshRenderer.sharedMaterials = materialsTemp;
            }
        }
    }
}
