using UnityEngine;

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
    foreach (var eye in objects)
    {
      this.SetColor(eye);
    }

    lastColor = customColor;
  }

  private void SetColor(GameObject eye)
  {
    // Get the Renderer component from the new cube
    var cubeRenderer = eye.GetComponent<Renderer>();

    cubeRenderer.material.SetColor(colorName, customColor);
  }
}
