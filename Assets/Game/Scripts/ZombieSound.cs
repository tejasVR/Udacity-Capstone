using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour {

    // 0 = left foot, 1 = right foot, 2 = head
    public AudioSource[] _audioBody;
   // public GameObject[] _bodyPart;

    public AudioClip[] _woodStep;
    //public AudioClip[] _idleSound;
    public AudioClip[] _walkingSound;
    public AudioClip[] _attackSound;
    public AudioClip[] _hitSound;

    public float pitchModulation;

    public void TakeStepOnWood(int bodyPart)
    {
        PlaySound.PlayAudioFromSelection(_audioBody[bodyPart], _woodStep, true, -.1f + pitchModulation, .1f + pitchModulation);
    }

    public void WalkingSound()
    {
        PlaySound.PlayAudioFromSelection(_audioBody[2], _walkingSound, true, -.1f + pitchModulation, .1f + pitchModulation);

    }

    public void AttackingSound()
    {
        PlaySound.PlayAudioFromSelection(_audioBody[2], _attackSound, true, -.1f + pitchModulation, .1f + pitchModulation);
    }

    public void HitSound()
    {
        PlaySound.PlayAudioFromSelection(_audioBody[2], _hitSound, true, -.1f + pitchModulation, .1f + pitchModulation);
    }

    //public static void DeathSound
}
