using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public static void PlayAudio(AudioSource source)
    {
        source.Play();
    }

    public static void PlayAudio(AudioSource source, bool randomPitch, float pitchRandMin, float pitchRandMax)
    {
        if (randomPitch)
            source.pitch = 1 + Random.Range(pitchRandMin, pitchRandMax);

        source.Play();
    }

    public static void PlayAudioFromSelection(AudioSource source, AudioClip[] clips, bool randomPitch, float pitchRandMin, float pitchRandMax)
    {
        if (randomPitch)
        {
            source.pitch = 1 + Random.Range(pitchRandMin, pitchRandMax);

        }

        var randomInt = Random.Range(0, clips.Length);

        source.PlayOneShot(clips[randomInt]);
    }
}
