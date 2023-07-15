using System;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  [Serializable]
  public class BufferV2
  {
    public BufferV2()
    {
      this.target = Vector2.zero;
      this.buffer = Vector2.zero;
    }

    public BufferV2(Vector2 targetInit, Vector2 bufferInit)
    {
      this.target = targetInit;
      this.buffer = bufferInit;
    }

    public Vector2 target;
    
    public Vector2 buffer;

    public Vector2 curDelta;  //Delta: apply difference from lastBuffer state to current BufferState		//get difference between last and new buffer
    
    public Vector2 curAbs;    //absolute

    public void UpdateByNewTarget(Vector2 newTarget, float dampLambda, float deltaTime)
    {
      this.target = newTarget;
      Update(dampLambda, deltaTime);
    }

    public void UpdateByDelta(Vector2 rawDelta, float dampLambda, float deltaTime)
    {
      this.target = this.target + rawDelta; //update Target
      Update(dampLambda, deltaTime);
    }

    public void Update(float dampLambda, float deltaTime, bool byPass = false)
    {
      Vector2 last = buffer;      //last state of Buffer
      this.buffer = byPass ? target : DampToTargetLambda(buffer, this.target, dampLambda, deltaTime); //damp current to target
      this.curDelta = buffer - last;
      this.curAbs = buffer;
    }

    public static Vector2 DampToTargetLambda(Vector2 current, Vector2 target, float lambda, float dt)
    {
      return Vector2.Lerp(current, target, 1F - Mathf.Exp(-lambda * dt));
    }
  }
}