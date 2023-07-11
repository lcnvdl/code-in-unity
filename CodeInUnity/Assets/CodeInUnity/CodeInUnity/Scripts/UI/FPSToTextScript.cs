using UnityEngine;
using UnityEngine.UI;

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
    [HideInInspector]
    private string format = "$FPS";

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];

        if (!string.IsNullOrEmpty(textComponent.text))
        {
            format = textComponent.text;
        }
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
                textComponent.text = format.Replace("$FPS", fps.ToString());
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
