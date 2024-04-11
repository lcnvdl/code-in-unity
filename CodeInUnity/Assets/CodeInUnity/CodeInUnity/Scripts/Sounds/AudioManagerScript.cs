using System.Collections;
using System.Collections.Generic;
using CodeInUnity.Core.Sounds;
using UnityEngine;

namespace CodeInUnity.Scripts.Sounds
{
  public class AudioManagerScript : MonoBehaviour
  {
    public List<AudioData> sounds = new List<AudioData>();

    private void Reset()
    {
      sounds = new List<AudioData>() { new AudioData() };
    }

    private void Awake()
    {
      foreach (var s in sounds)
      {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.basePitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = s.spatialBlend;
        s.source.panStereo = s.pan;
        s.source.playOnAwake = false;
      }
    }

    public void Play(string name)
    {
      Play(GetSound(name));
    }

    public void PlayDelayed(string name, float delay)
    {
      StartCoroutine(PlayDelayedCoroutine(name, delay));
    }

    private IEnumerator PlayDelayedCoroutine(string name, float delay)
    {
      yield return new WaitForSeconds(delay);

      Play(name);
    }

    public AudioData PlayAndGet(string name)
    {
      var sound = GetSound(name);

      Play(sound);

      return sound;
    }

    public void Play(AudioData sound)
    {
      if (sound == null)
      {
        return;
      }

      if (sound.randomizePitch)
      {
        sound.source.pitch = sound.basePitch * UnityEngine.Random.Range(sound.randomPitchRange.x, sound.randomPitchRange.y);
      }

      sound.source.volume = sound.volume;

      if (sound.source.enabled)
      {
        sound.source.Play();
      }
    }

    public void Stop(string name)
    {
      var sound = GetSound(name);

      if (sound != null)
      {
        sound.source.Stop();
      }
    }

    public AudioData GetSound(string name)
    {
      foreach (AudioData sound in sounds)
      {
        if (sound.name == name)
        {
          return sound;
        }
      }

      Debug.LogWarning($"Sound {name} not found");

      return null;
    }
  }
}