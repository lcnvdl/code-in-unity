using UnityEngine;

namespace CodeInUnity.Extensions
{
  public static class MaterialExtensions
  {
    public static Material Clone(this Material material)
    {
      return new Material(material);
    }
  }
}
