using System;
using UnityEngine;

namespace CodeInUnity.Core.Security
{
  [Serializable]
  public class FairRandom
  {
    public bool preventTooMuchLuck = true;

    [SerializeField]
    private float probability; // Probabilidad base (ejemplo: 0.1 para 10%)

    [SerializeField]
    [HideInInspector]
    private int maxAttempts;   // Intentos máximos antes de forzar un acierto

    private int attempts;      // Contador de intentos fallidos

    private int luckyNumber;   // Número de intentos antes del acierto forzado

    public FairRandom()
    {
    }

    public FairRandom(float probability)
    {
      this.Initialize(probability);
    }

    public bool GetNextTry(float probability)
    {
      if (this.probability != probability)
      {
        this.Initialize(probability);
      }

      return this.GetNextTry();
    }

    public bool GetNextTry()
    {
      if (attempts >= luckyNumber)
      {
        this.Reset();

        //  Increases the difficulty a little bit because the player should not have too much luck

        if (this.preventTooMuchLuck && this.attempts < this.maxAttempts)
        {
          this.luckyNumber += this.maxAttempts - this.attempts;
        }

        return true;
      }

      if (UnityEngine.Random.value < probability)
      {
        this.Reset();
        return true;
      }

      this.attempts++;
      return false;
    }

    private void Initialize(float probability)
    {
      this.probability = Mathf.Clamp01(probability);
      this.maxAttempts = Mathf.Max(1, Mathf.RoundToInt(1f / probability));
      this.Reset();
    }

    private void SetNewLuckyNumber()
    {
      this.luckyNumber = UnityEngine.Random.Range(maxAttempts / 2, maxAttempts); // Varía el "número de la suerte"
    }

    private void Reset()
    {
      this.attempts = 0;
      this.SetNewLuckyNumber();
    }
  }
}
