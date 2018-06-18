using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundSource : MonoBehaviour {



    private AudioSource _audioSource;

    public enum ObjectType
    {
        cup,
        glass,
        chair,
        wood,
        metal
    }

    public ObjectType _objectType;

    private void OnCollisionEnter(Collision collision)
    {
        switch (_objectType)
        {
            case ObjectType.cup:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds.instance._cupSounds, true, -.05f, .05f);
                break;

            case ObjectType.glass:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds.instance._glassSounds, true, -.05f, .05f);
                break;

            case ObjectType.chair:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds.instance._chairSounds, true, -.05f, .05f);
                break;

            case ObjectType.wood:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds.instance._woodSounds, true, -.15f, .15f);
                break;

            case ObjectType.metal:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds.instance._metalSounds, true, -.05f, .05f);
                break;
        }
    }
}
