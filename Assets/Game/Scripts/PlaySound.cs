using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public static void PlayAudio(AudioSource source)
    {
        source.Play();
    }

    public static void PlayAudio(AudioSource source, bool randomPitch)
    {
        if (randomPitch)
            source.pitch = 1 + Random.Range(-.1f, .1f);

        source.Play();
    }

    public static void PlayAudioFromSelection(AudioSource source, AudioClip[] clips, bool randomPitch)
    {
        if (randomPitch)
        {
            var randomFloat = Random.Range(-1f, .1f);
            source.pitch = 1 + randomFloat;
        }

        var randomInt = Random.Range(0, clips.Length);

        source.PlayOneShot(clips[randomInt]);
    }
}
