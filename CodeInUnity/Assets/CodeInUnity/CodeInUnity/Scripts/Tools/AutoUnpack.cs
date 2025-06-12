using UnityEngine;

namespace CodeInUnity.Scripts.Tools
{
  /// <summary>
  /// Automatically unpacks all child GameObjects of this GameObject at startup by moving them to the parent transform.
  /// 
  /// Optionally renames each child to include this GameObject's name as a prefix if 'setFolderName' is enabled.
  /// 
  /// Useful for flattening prefab hierarchies or organizing scene objects in Unity.
  /// </summary>
  public class AutoUnpack : MonoBehaviour
  {
    [SerializeField]
    private bool setFolderName = false;

    [SerializeField]
    private bool autoDestroyAfterUnfold = false;

    [SerializeField]
    private bool onlyForProduction = false;

    void Start()
    {
      if (this.onlyForProduction && Application.isEditor)
      {
        return;
      }

      var parent = transform.parent;
      int insertIndex = transform.GetSiblingIndex() + 1;
      int totalChildren = transform.childCount;

      for (int i = 0; i < totalChildren; ++i)
      {
        var child = transform.GetChild(0);

        if (setFolderName)
        {
          child.name = $"{name}.{child.name}";
        }

        child.SetParent(parent, true);
        child.SetSiblingIndex(insertIndex++);
      }

      if (this.autoDestroyAfterUnfold)
      {
        Destroy(gameObject);
      }
      else
      {
        gameObject.SetActive(false);
      }
    }
  }
}