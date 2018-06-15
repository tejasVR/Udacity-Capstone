using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour {

    //public enum DoorType
    //{
    //    woodDoor,
    //    metalDoor,
    //    heavyDoor
    //}

    //public DoorType _doorType;

    // For audioSource, 0 = hits/hand, 1 = open
    public AudioSource[] _audioSource;
    public VA_AudioSource[] vaAudio;


    [Header("Door Sounds")]
    public AudioClip[] _doorOpenClips;
    public AudioClip[] _doorHitClips;
    public AudioClip[] _doorHandClips;

    //[Header("Wood Door Sounds")]
    //public AudioClip[] _woodDoorOpenClips;
    //public AudioClip[] _woodDoorHitClips;
    //public AudioClip[] _woodDoorHandClips;

    //[Header("Metal Door Sounds")]
    //public AudioClip[] _metalDoorOpenClips;
    //public AudioClip[] _metalDoorHitClips;
    //public AudioClip[] _metalDoorHandClips;

    //[Header("Heavy Door Sounds")]
    //public AudioClip[] _heavyDoorOpenClips;
    //public AudioClip[] _heavyDoorHitClips;
    //public AudioClip[] _heavyDoorHandClips;

    private Rigidbody rb;


    [Header("Door Open Pitch Controls")]
    public float pitchModulationAmount = .2f;
    private float pitchMaster;
    private float pitchOriginal;

    private float volumeMaster;
    private float volumeOriginal;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //vaAudioOpen = GetComponent<VA_AudioSource>();

        pitchOriginal = _audioSource[1].pitch;
        volumeOriginal = _audioSource[1].volume;
        //volumeOriginal = vaAudio[1].BaseVolume;

	}
	
	// Update is called once per frame
	void Update () {
        //print(rb.angularVelocity.magnitude);

        if (rb.angularVelocity.magnitude > .15)
        {
            pitchMaster = pitchOriginal + pitchModulationAmount / 3;
            volumeMaster = volumeOriginal;
        }
        else if (rb.angularVelocity.magnitude > .25)
            pitchMaster = pitchOriginal + pitchModulationAmount / 2;
        else if (rb.angularVelocity.magnitude > .5)
            pitchMaster = pitchOriginal + pitchModulationAmount;
        else
        {
            pitchMaster = pitchOriginal;
            volumeMaster = 0;
        }



        _audioSource[1].pitch = Mathf.Lerp(_audioSource[1].pitch, pitchMaster, Time.deltaTime);// * 5f);
        _audioSource[1].volume = Mathf.Lerp(_audioSource[1].volume, volumeMaster, Time.deltaTime * 3f);
        //vaAudio[1].BaseVolume = Mathf.Lerp(vaAudio[1].BaseVolume, volumeMaster, Time.deltaTime * 3f);


        //print(pitchMaster);


    }

    private void OnCollisionEnter(Collision collision)
    {
        //switch (_doorType)
        //{
        //    case DoorType.woodDoor:
        //        if (collision.gameObject.tag == "Player")
        //            PlaySound(_audioSource[0], _woodDoorHandClips[0], true);
        //        else
        //            PlaySound(_audioSource[0], _woodDoorHitClips[0], true);
        //        break;

        //    case DoorType.metalDoor:
        //        if (collision.gameObject.tag == "Player")
        //            PlaySound(_audioSource[0], _metalDoorHandClips[0], true);
        //        else
        //            PlaySound(_audioSource[0], _metalDoorHitClips[0], true);
        //        break;

        //    case DoorType.heavyDoor:
        //        if (collision.gameObject.tag == "Player")
        //            PlaySound(_audioSource[0], _heavyDoorHandClips[0], true);
        //        else
        //            PlaySound(_audioSource[0], _heavyDoorHitClips[0], true);
        //        break;
        //}

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Controller"))
        {
            PlaySound.PlayAudioFromSelection(_audioSource[0], _doorHandClips, true, -.1f, .1f);

            if (!_audioSource[1].isPlaying)
            {
                PlaySound.PlayAudioFromSelection(_audioSource[1], _doorOpenClips, false, -.1f, .1f);
            }

        }
        else
            PlaySound.PlayAudioFromSelection(_audioSource[0], _doorHitClips, true, -.1f, .1f);

        //print("hit door on object:" + collision.gameObject.name);
    }


    //private void PlaySound(AudioSource source, AudioClip[] clips, bool randomPitch)
    //{
    //    if (randomPitch)
    //    {
    //        //var pitchOriginal = source.pitch;
    //        var randomFloat = Random.Range(-1f, .1f);
    //        source.pitch = 1 + randomFloat;
    //    }

    //    var randomInt = Random.Range(0, clips.Length);
        
    //    source.PlayOneShot(clips[randomInt]);

    //    //print("played sound:" + clips[randomInt].name);

    //}


    //private void DoorOpen(AudioSource source, bool randomPitch)
    //{
    //    source.enabled = true;

    //    if (randomPitch)
    //    {
    //        var randomFloat = Random.Range(-1f, .1f);
    //        source.pitch += randomFloat;
    //    }

    //    source.Play();
    //}

    //private void DoorStop(AudioSource source)
    //{
    //    source.Stop();
    //}
}
