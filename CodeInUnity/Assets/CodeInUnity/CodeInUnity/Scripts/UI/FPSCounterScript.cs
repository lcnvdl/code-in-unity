using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.UI
{
  public class FPSCounterScript : MonoBehaviour
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

    public UnityEvent<int> updateFPS;

    private void Awake()
    {
      frameDeltaTimeArray = new float[50];
    }

    private void Update()
    {
      frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
      lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

      if (lastFrameIndex == 0)
      {
        int fps = Mathf.RoundToInt(this.CalculateFPS());
        if (lastFPS != fps)
        {
          updateFPS?.Invoke(fps);
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
