using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Sprites
{
  public class FadeOutSprite : ActionScript
  {
    public SpriteRenderer sprite;

    public float timeInSeconds = 1f;

    public UnityEvent onFinish;

    [HideInInspector]
    [SerializeField]
    private float totalTime = 0f;

    [HideInInspector]
    [SerializeField]
    private float initialAlpha = 0f;

    [HideInInspector]
    [SerializeField]
    private bool isEnabled = false;

    [HideInInspector]
    [SerializeField]
    private bool isFinished = false;

    public void CallAction()
    {
      ExecuteAction();
    }

    protected override void Run()
    {
      this.isEnabled = true;
      this.initialAlpha = sprite.color.a;
    }

    private void Update()
    {
      if (isEnabled)
      {
        if (!this.isFinished)
        {
          this.totalTime += Time.deltaTime;

          if (this.totalTime >= this.timeInSeconds)
          {
            this.totalTime = this.timeInSeconds;
            this.isFinished = true;

            this.onFinish.Invoke();
          }

          float targetAlpha = this.initialAlpha - (this.initialAlpha * this.totalTime / this.timeInSeconds);

          this.sprite.color = new Color(this.sprite.color.r, this.sprite.color.g, this.sprite.color.b, targetAlpha);
        }
      }
    }
  }
}