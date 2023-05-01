using System.Collections.Generic;
using System.Linq;
using CodeInUnity.Extensions;
using UnityEngine;
using UnityEngine.Rendering;

public enum GPUMeshChildrenAutoOptimizeAfterLoadAction
{
    NoAction,
    Deactivate,
    Destroy
}

public class GPUMeshChildrenAutoOptimizeScript : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    private List<GPUMeshAutoOptimizeScript> children = new List<GPUMeshAutoOptimizeScript>();

    public GPUMeshChildrenAutoOptimizeAfterLoadAction afterLoad = GPUMeshChildrenAutoOptimizeAfterLoadAction.NoAction;

    public bool isOptimizing = false;

    public ShadowCastingMode shadowCastingMode;

    public bool receiveShadows;

    public void BeginOptimization(Transform[] instancesToOptimize)
    {
        this.children.ForEach(m => Destroy(m));
        this.children.Clear();

        List<Transform> alreadyOptimized = new List<Transform>();

        //  Prepare
        foreach (var instance in instancesToOptimize)
        {
            if (alreadyOptimized.Contains(instance))
            {
                continue;
            }

            if (instance.TryGetComponent<MeshFilter>(out var meshFilter))
            {
                string meshId = meshFilter.GetCustomTag("mesh_id")?.additionalValue ?? string.Empty;

                if (!this.children.Any(m => m.mesh == meshFilter.mesh || m.HasCustomTag("mesh_id", meshId)))
                {
                    var transforms = instancesToOptimize.Where(m => m.HasCustomTag("mesh_id", meshId) || m.GetComponent<MeshFilter>().mesh == meshFilter.mesh).ToArray();
                    Debug.Log("Added transforms for mesh " + meshFilter.mesh.name + ". Count: " + transforms.Length + ". Mesh: " + meshId);
                    var newChild = this.gameObject.AddComponent<GPUMeshAutoOptimizeScript>();
                    newChild.afterLoad = this.afterLoad;
                    newChild.receiveShadows = this.receiveShadows;
                    newChild.shadowCastingMode = this.shadowCastingMode;
                    newChild.BeginOptimization(transforms);
                    alreadyOptimized.AddRange(transforms);
                    this.children.Add(newChild);
                }
            }
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
        for (int i = 0; i < this.children.Count; i++)
        {
            this.children[i].isOptimizing = this.isOptimizing;
        }
    }
}
