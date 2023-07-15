using System;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public static class SoftPitchClamping
  {
    public static float DeltaMod(float currentPitch, float delta, float softMax = 45F, float hardMax = 85F)
    {

      //doesnt work for input above 90F pitch, unity might invert roll and decrease pitch again

      //transform into -180 to 180 range (allowed input range = 0-360F )
      float wrapped = Wrap.Float(currentPitch, -180F, 180F);

      float sign = Mathf.Sign(wrapped);
      float absolute = Mathf.Abs(wrapped);

      //	treat current as mapped value, so unmap it via reversing
      //	https://rechneronline.de/function-graphs/
      //	revert remap:	e^((((log(x/45)+1)*45)/45)-1)*45
      //	remap:	(log(x/45)+1)*45

      float remapped = absolute;
      if (absolute > softMax)
      {
        //				 e^ ((			((		  log(       x/45	  )+1  )*45		 )		/45		) -1 )*45
        //	remapped = Mathf.Exp((			((	Mathf.Log(remapped/softMax)+1F )*softMax )		/softMax) -1F)*softMax ;
        remapped = Mathf.Exp((remapped / softMax) - 1F) * softMax;
        //x*0.5+45*0.5
      }

      //apply delta to unmapped, sign needs to be taken into consideration for delta
      remapped += (delta * sign); //float raw = remapped +(delta *sign);

      //remap, only needed if exceeding softrange
      if (remapped > softMax)
      {
        //								((		 log(		 x/45	 )+1  )*45		)
        remapped = ((Mathf.Log(remapped / softMax) + 1F) * softMax);

        //x*0.5+45*0.5

      }

      remapped *= sign;
      remapped = Mathf.Clamp(remapped, -hardMax, +hardMax);

      float newDelta = (remapped - wrapped);

      //print( "wrapped\t"+wrapped+" (from:"+currentPitch+")"+"\nremapped\t"+remapped +" (raw :"+raw+")");

      return newDelta;
      //	return delta;
    }

    public static class Wrap
    {

      //can be used to clamp angles from 0-360 to 0-180 by feeding (value,-180,180)
      //https://stackoverflow.com/questions/1628386/normalise-orientation-between-0-and-360

      //Normalizes any number to an arbitrary range 
      //by assuming the range wraps around when going below min or above max 
      public static float Float(float value, float start, float end)
      {
        float width = end - start;   // 
        float offsetValue = value - start;   // value relative to 0

        return (offsetValue - (Mathf.Floor(offsetValue / width) * width)) + start;
        // + start to reset back to start of original range
      }

      //Normalizes any number to an arbitrary range 
      //by assuming the range wraps around when going below min or above max 
      public static int Int(int value, int start, int end)
      {
        int width = end - start;   // 
        int offsetValue = value - start;   // value relative to 0

        return (offsetValue - ((offsetValue / width) * width)) + start;
        // + start to reset back to start of original range
      }
    }
  }
}