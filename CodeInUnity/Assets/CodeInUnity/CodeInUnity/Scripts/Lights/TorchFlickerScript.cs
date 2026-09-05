using UnityEngine;

namespace CodeInUnity.Scripts.Lights
{
  public class TorchFlickerScript : MonoBehaviour
  {
    [Range(0, 10)]
    public int lod = 0;

    public Light torchLight;

    public float minIntensity = 1.0f;
    public float maxIntensity = 1.4f;

    public float minRange = 5.0f;
    public float maxRange = 6.0f;

    public float speed = 8.0f;

    private float seed;
    private int iterationLod;

    private void Start()
    {
      this.seed = Random.Range(0.0f, 100.0f);
    }

    private void OnValidate()
    {
      if (this.torchLight == null)
      {
        this.torchLight = this.GetComponent<Light>();
      }
    }

    private void Update()
    {
      if (this.lod > 0)
      {
        if (this.iterationLod < this.lod)
        {
          this.iterationLod++;
          return;
        }
        else
        {
          this.iterationLod = 0;
        }
      }

      float noise = Mathf.PerlinNoise(this.seed, Time.time * this.speed);

      this.torchLight.intensity = Mathf.Lerp(
          this.minIntensity,
          this.maxIntensity,
          noise
      );

      this.torchLight.range = Mathf.Lerp(
          this.minRange,
          this.maxRange,
          noise
      );
    }
  }
}