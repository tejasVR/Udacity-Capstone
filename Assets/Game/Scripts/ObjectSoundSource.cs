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

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (_objectType)
        {
            case ObjectType.cup:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds._instance._cupSounds, true, -.05f, .05f);
                //print(_audioSource.gameObject.name);
                break;

            case ObjectType.glass:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds._instance._glassSounds, true, -.05f, .05f);
                break;

            case ObjectType.chair:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds._instance._chairSounds, true, -.05f, .05f);
                break;

            case ObjectType.wood:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds._instance._woodSounds, true, -.15f, .15f);
                break;

            case ObjectType.metal:
                PlaySound.PlayAudioFromSelection(_audioSource, ObjectSounds._instance._metalSounds, true, -.05f, .05f);
                break;
        }
    }
}
