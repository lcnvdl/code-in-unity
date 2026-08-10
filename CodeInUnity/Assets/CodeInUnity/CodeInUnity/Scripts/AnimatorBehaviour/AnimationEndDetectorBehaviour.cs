using System;
using UnityEngine;

namespace CodeInUnity.Scripts.AnimatorBehaviour
{
  public class AnimationEndDetectorBehaviour : StateMachineBehaviour
  {
    public static event Action<Animator, int> OnAnimationEnd;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      OnAnimationEnd?.Invoke(animator, stateInfo.shortNameHash);
    }
  }
}