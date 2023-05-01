using UnityEngine;

public enum OcclusionStatus
{
    Enabled,
    Disabled
}

public class OcclusionToggler : MonoBehaviour
{
    public OcclusionStatus status;

    public bool selfApply = true;

    public bool recursive = true;

    void Start()
    {
        if (this.selfApply)
        {
            if (TryGetComponent<MeshRenderer>(out var mesh))
            {
                mesh.allowOcclusionWhenDynamic = (status == OcclusionStatus.Enabled);
            }
        }

        if (this.recursive)
        {
            foreach (var mesh in GetComponentsInChildren<MeshRenderer>())
            {
                mesh.allowOcclusionWhenDynamic = (status == OcclusionStatus.Enabled);
            }
        }
    }
}
