using System.Collections.Generic;
using CodeInUnity.Scripts.Sounds;
using UnityEngine;
using UnityEngine.Audio;

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
            s.source.playOnAwake = false;
        }
    }

    public void Play(string name)
    {
        Play(GetSound(name));
    }

    public AudioData PlayAndGet(string name)
    {
        var sound = GetSound(name);

        Play(sound);

        return sound;
    }

    public void Play(AudioData sound)
    {
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
        var sound = sounds.Find(m => m.name == name);
        return sound;
    }
}
