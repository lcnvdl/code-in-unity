using CodeInUnity.Core.Utils;
using UnityEngine;

namespace CodeInUnity.Scripts.Actions.CharacterController
{
  //Marrt's simplest Mouselook for https://forum.unity.com/threads/a-free-simple-smooth-mouselook.73117/page-2
  public class MarrtsSmoothedMouseLook : MonoBehaviour
  {

    [Header("CameraTransform")]
    public Transform targetTrans;

    [Header("On/Off & Settings")]
    public bool inputActive = true;

    public bool controlCursor = false;

    [Header("Smoothing")]
    public bool byPassSmoothing = false;

    public float mLambda = 20F; //higher = less latency but also less smoothing

    [Header("Sensitivity")]
    public float hSens = 4F;

    public float vSens = 4F;

    public BufferV2 mouseBuffer = new BufferV2();

    public enum AxisClampMode { None, Hard, Soft }

    [Header("Restricting Look")]

    public AxisClampMode pitchClamp = AxisClampMode.Soft;

    [Range(-180F, 180F)] public float pMin = -80F;

    [Range(-180F, 180F)] public float pMax = 80F;


    [Header("Yaw should be left None, Message me if you really need this functionality")]

    public AxisClampMode yawClamp = AxisClampMode.None;

    [Range(-180F, 180F)] public float yMin = -140F;

    [Range(-180F, 180F)] public float yMax = 140F;

    //public	bool		smoothingDependence	= Timescale Framerate

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
    //void LateUpdate() { }

    private void UpdateMouseBuffer()
    {
      float rawPitchDelta = vSens * -Input.GetAxisRaw("Mouse Y");

      switch (pitchClamp)
      {
        case AxisClampMode.None: mouseBuffer.target.x += rawPitchDelta; break;
        case AxisClampMode.Hard: mouseBuffer.target.x = Mathf.Clamp(mouseBuffer.target.x + rawPitchDelta, pMin, pMax); break;
        case AxisClampMode.Soft: mouseBuffer.target.x += SoftPitchClamping.DeltaMod(mouseBuffer.target.x, rawPitchDelta, Mathf.Abs(pMax * 0.5F), Mathf.Abs(pMax)); break; //symetric clamping only for now, max is used
      }

      float rawYawDelta = hSens * Input.GetAxisRaw("Mouse X");

      switch (yawClamp)
      {
        case AxisClampMode.None: mouseBuffer.target.y += rawYawDelta; break;
        case AxisClampMode.Hard: mouseBuffer.target.y = Mathf.Clamp(mouseBuffer.target.y + rawYawDelta, yMin, yMax); break;
        case AxisClampMode.Soft:
          Debug.LogWarning("SoftYaw clamp should be implemented with Quaternions to work in every situation");
          mouseBuffer.target.y += SoftPitchClamping.DeltaMod(mouseBuffer.target.y, rawYawDelta, Mathf.Abs(yMax * 0.5F), Mathf.Abs(yMax));
          break;
      }

      mouseBuffer.Update(mLambda, Time.deltaTime, byPassSmoothing);
    }
  }
}