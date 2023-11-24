using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderScript : MonoBehaviour
{
  private static FaderScript instance;

  private static FaderScript Instance
  {
    get
    {
      if (instance == null)
      {
        instance = FindAnyObjectByType<FaderScript>(FindObjectsInactive.Include);
      }

      return instance;
    }
  }

  public bool onlyFadeIn = false;

  public float duration = 1f;

  public float hodl = 0f;

  private float DurationPerFace => this.onlyFadeIn ? duration : duration / 2;

  public Color fadeColor = Color.black;

  [SerializeField]
  private Image blackImage;

  [SerializeField]
  [HideInInspector]
  private float currentAlpha = 0f;

  [SerializeField]
  [HideInInspector]
  private float currentHodl = 0f;

  private void OnValidate()
  {
    blackImage = GetComponentInChildren<Image>();
  }

  private void OnEnable()
  {
    instance = this;

    currentAlpha = this.onlyFadeIn ? 1f : 0f;

    currentHodl = 0f;

    UpdateColor();
  }

  private void Update()
  {
    if (onlyFadeIn)
    {
      currentAlpha = Mathf.Clamp01(currentAlpha - Time.deltaTime / DurationPerFace);
    }
    else
    {
      currentAlpha = Mathf.Clamp01(currentAlpha + Time.deltaTime / DurationPerFace);
    }

    UpdateColor();
  }

  private void LateUpdate()
  {
    if (onlyFadeIn)
    {
      if (currentAlpha <= 0)
      {
        gameObject.SetActive(false);
      }
    }
    else
    {
      if (currentAlpha >= 1)
      {
        if (currentHodl < hodl)
        {
          currentHodl += Time.deltaTime;
        }
        else
        {
          onlyFadeIn = true;
        }
      }
    }
  }

  public static void FadeOutIn(float duration = 1, float hodl = 0f) => FadeOutIn(Color.black, duration, hodl);

  public static void FadeOutIn(Color color, float duration, float hodl)
  {
    FaderScript.Instance.gameObject.SetActive(false);
    FaderScript.Instance.hodl = hodl;
    FaderScript.Instance.duration = duration;
    FaderScript.Instance.onlyFadeIn = false;
    FaderScript.Instance.fadeColor = color;
    FaderScript.Instance.gameObject.SetActive(true);
  }

  public static void FadeIn(float duration = 1f) => FadeIn(Color.black, duration);

  public static void FadeIn(Color color, float duration)
  {
    FaderScript.Instance.gameObject.SetActive(false);
    FaderScript.Instance.hodl = 0;
    FaderScript.Instance.duration = duration;
    FaderScript.Instance.onlyFadeIn = true;
    FaderScript.Instance.fadeColor = color;
    FaderScript.Instance.gameObject.SetActive(true);
  }

  private void UpdateColor()
  {
    blackImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, currentAlpha);
  }
}
