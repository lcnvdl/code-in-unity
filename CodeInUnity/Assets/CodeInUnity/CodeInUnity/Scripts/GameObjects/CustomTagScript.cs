#if UNITY_2022_1_OR_NEWER
using System;
#endif

using UnityEngine;

namespace CodeInUnity.Scripts.GameObjects
{
  public class CustomTagScript : MonoBehaviour
  {
    public string tagId;

    public string additionalValue;

    public static CustomTagScript AddTag(GameObject gameObject, string tagId, string additionalValue = null)
    {
      var tagScript = gameObject.AddComponent<CustomTagScript>();
      tagScript.tagId = tagId;
      tagScript.additionalValue = additionalValue;
      return tagScript;
    }

    public static bool IsTagged(GameObject gameObject, string tagId)
    {
      if (gameObject == null)
      {
        return false;
      }

#if UNITY_2022_1_OR_NEWER
      int count = gameObject.GetComponentCount();

      for (int i = 0; i < count; i++)
      {
        var tagScript = gameObject.GetComponentAtIndex(i) as CustomTagScript;

        if (tagScript != null && tagScript.tagId == tagId)
        {
          return true;
        }
      }

      return false;
#else
      var tagScripts = gameObject.GetComponents<CustomTagScript>();

      foreach (var tagScript in tagScripts)
      {
        if (tagScript.tagId == tagId)
        {
          return true;
        }
      }

      return false;
#endif
    }
  }
}
