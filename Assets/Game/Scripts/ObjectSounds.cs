using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSounds : MonoBehaviour {

    public static ObjectSounds _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }

    //private AudioSource _audioSource;

    //public enum ObjectType
    //{
    //    cup,
    //    glass,
    //    chair,
    //    wood,
    //    metal
    //}

    //public ObjectType _objectType;

    public AudioClip[] _cupSounds;
    public AudioClip[] _glassSounds;
    public AudioClip[] _chairSounds;
    public AudioClip[] _woodSounds;
    public AudioClip[] _metalSounds;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    switch (_objectType)
    //    {
    //        case ObjectType.cup:
    //            PlaySound.PlayAudioFromSelection(_audioSource, _cupSounds, true, -.05f, .05f);
    //            break;

    //        case ObjectType.glass:
    //            PlaySound.PlayAudioFromSelection(_audioSource, _glassSounds, true, -.05f, .05f);
    //            break;

    //        case ObjectType.chair:
    //            PlaySound.PlayAudioFromSelection(_audioSource, _chairSounds, true, -.05f, .05f);
    //            break;

    //        case ObjectType.wood:
    //            PlaySound.PlayAudioFromSelection(_audioSource, _woodSounds, true, -.05f, .05f);
    //            break;

    //        case ObjectType.metal:
    //            PlaySound.PlayAudioFromSelection(_audioSource, _metalSounds, true, -.05f, .05f);
    //            break;
    //    }
    //}

}
