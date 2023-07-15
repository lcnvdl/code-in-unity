using CodeInUnity.Core.Utils;
using UnityEngine;

//Marrt's simplest Mouselook for https://forum.unity.com/threads/a-free-simple-smooth-mouselook.73117/page-2

public class SimplestSmoothedMouseLook : MonoBehaviour
{

  [Header("CameraTransform")]
  public Transform targetTrans;

  [Header("On/Off & Settings")]
  public bool inputActive = true;

  public bool controlCursor = false;

  public bool pitchClamp = true;

  [Header("Smoothing")]
  public bool byPassSmoothing = false;

  public float mLambda = 20F; //higher = less latency but also less smoothing

  [Header("Sensitivity")]
  public float hSens = 4F;

  public float vSens = 4F;

  public BufferV2 mouseBuffer = new BufferV2();

  void Update()
  {
    //if(Input.GetKeyDown(KeyCode.Space)){inputActive = !inputActive;}
    if (controlCursor)
    { //Cursor Control
      if (inputActive && Cursor.lockState != CursorLockMode.Locked) { Cursor.lockState = CursorLockMode.Locked; }
      if (!inputActive && Cursor.lockState != CursorLockMode.None) { Cursor.lockState = CursorLockMode.None; }
    }
    if (!inputActive) { return; } //active?

    //Update input
    UpdateMouseBuffer();
    targetTrans.rotation = Quaternion.Euler(mouseBuffer.curAbs);
  }

  //consider late Update for applying the rotation if your game needs it (e.g. if camera parents are rotated in Update for some reason)
  // void LateUpdate() { }

  private void UpdateMouseBuffer()
  {
    mouseBuffer.target += new Vector2(vSens * -Input.GetAxisRaw("Mouse Y"), hSens * Input.GetAxisRaw("Mouse X"));//Mouse Input is inherently framerate independend!
    mouseBuffer.target.x = pitchClamp ? Mathf.Clamp(mouseBuffer.target.x, -80F, +80F) : mouseBuffer.target.x;
    mouseBuffer.Update(mLambda, Time.deltaTime, byPassSmoothing);
  }
}
