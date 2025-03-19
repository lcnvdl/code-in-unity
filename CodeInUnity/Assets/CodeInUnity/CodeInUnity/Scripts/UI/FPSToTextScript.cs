using UnityEngine;
using UnityEngine.UI;

namespace CodeInUnity.Scripts.UI
{
  public class FPSToTextScript : MonoBehaviour
  {
    [SerializeField]
    [HideInInspector]
    private int lastFrameIndex;

    [SerializeField]
    [HideInInspector]
    private int lastFPS = -1;

    [SerializeField]
    [HideInInspector]
    private float[] frameDeltaTimeArray;

    [SerializeField]
    private Text textComponent;

    [SerializeField]
    private string format = "$FPS";

    [SerializeField]
    private int samples = 50;

    private void Awake()
    {
      frameDeltaTimeArray = new float[samples];
    }

    private void Update()
    {
      frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
      lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

      if (lastFrameIndex == 0)
      {
        int fps = Mathf.RoundToInt(this.CalculateFPS());
        if (lastFPS != fps)
        {
          textComponent.text = string.IsNullOrEmpty(format) ? fps.ToString() : format.Replace("$FPS", fps.ToString());
          lastFPS = fps;
        }
      }
    }

    private float CalculateFPS()
    {
      float total = 0f;

      foreach (float dt in frameDeltaTimeArray)
      {
        total += dt;
      }

      return frameDeltaTimeArray.Length / total;
    }
  }
}
