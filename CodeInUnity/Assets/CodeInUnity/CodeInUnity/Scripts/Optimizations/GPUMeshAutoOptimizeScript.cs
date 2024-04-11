using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CodeInUnity.Scripts.Optimizations
{
  public class GPUMeshAutoOptimizeScript : MonoBehaviour
  {
    public UnityEngine.Mesh mesh;

    public Material[] materials;

    public Transform[] instancesToOptimize;

    public GPUMeshChildrenAutoOptimizeAfterLoadAction afterLoad = GPUMeshChildrenAutoOptimizeAfterLoadAction.NoAction;

    [HideInInspector]
    public MaterialPropertyBlock materialPropertyBlock;

    private List<List<Matrix4x4>> batches = new List<List<Matrix4x4>>();

    public bool isOptimizing = false;

    public ShadowCastingMode shadowCastingMode;

    public bool receiveShadows;

    private void Start()
    {
      materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void BeginOptimization(Transform[] instancesToOptimize)
    {
      //  Reset
      this.batches.Clear();
      this.instancesToOptimize = instancesToOptimize;
      this.mesh = null;
      this.materials = null;

      //  Prepare
      foreach (var instance in instancesToOptimize)
      {
        var meshRenderer = instance.GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
          if (this.mesh == null)
          {
            var meshFilter = instance.GetComponent<MeshFilter>();
            this.mesh = meshFilter.mesh;
            this.materials = meshRenderer.materials;
          }

          meshRenderer.enabled = false;
        }

        if (afterLoad == GPUMeshChildrenAutoOptimizeAfterLoadAction.Deactivate)
        {
          instance.gameObject.SetActive(false);
        }
        else if (afterLoad == GPUMeshChildrenAutoOptimizeAfterLoadAction.Deactivate)
        {
          Destroy(instance.gameObject);
        }
      }

      //  Generate batches
      int addedMatricies = 0;

      for (int i = 0; i < instancesToOptimize.Length; i++)
      {
        if (addedMatricies % 1000 == 0)
        {
          //Debug.Log("Matrix chunk added at " + i);
          batches.Add(new List<Matrix4x4>());
        }

        batches[batches.Count - 1].Add(Matrix4x4.TRS(
            instancesToOptimize[i].transform.position,
            instancesToOptimize[i].transform.rotation,
            instancesToOptimize[i].transform.lossyScale));
      }

      //  Start
      this.isOptimizing = true;
    }

    private void Update()
    {
      if (this.isOptimizing)
      {
        RenderBatches();
      }
    }

    private void RenderBatches()
    {
      foreach (var batch in batches)
      {
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
          Graphics.DrawMeshInstanced(mesh, i, materials[i], batch, materialPropertyBlock, shadowCastingMode, receiveShadows);
        }
      }
    }
  }
}
