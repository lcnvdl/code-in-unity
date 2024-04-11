using UnityEngine;

namespace CodeInUnity.Scripts.Mesh
{
  public class MeshMaterialColorScript : MonoBehaviour
  {
    public GameObject[] objects;

    public Color customColor = Color.black;

    public string colorName = "_BaseColor";

    private Color lastColor = Color.black;

    void Start()
    {
      this.ApplyColor();
    }

    void Update()
    {
      if (lastColor != customColor)
      {
        this.ApplyColor();
      }
    }

    private void ApplyColor()
    {
      foreach (var target in objects)
      {
        this.SetColor(target);
      }

      lastColor = customColor;
    }

    private void SetColor(GameObject target)
    {
      var renderer = target.GetComponent<Renderer>();

      renderer.material.SetColor(colorName, customColor);
    }
  }
}