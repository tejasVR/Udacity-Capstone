using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour {

    public enum DoorType
    {
        woodDoor,
        metalDoor,
        heavyDoor
    }

    public DoorType _doorType;

    // For audioSource, 0 = hits, 1 = open
    private AudioSource[] _audioSource;

    // For clips, 0 = hit, 1 = open, 2  unlock
    public AudioClip[] _woodDoorClips;
    public AudioClip[] _metalDoorClips;
    public AudioClip[] _heavyDoorClips;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        print(rb.angularVelocity.magnitude);

        if (rb.angularVelocity.magnitude > .35)
            DoorOpen(_audioSource[1], true);
        else
            DoorStop(_audioSource[1]);

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (_doorType)
        {
            case DoorType.woodDoor:
                PlaySound(_audioSource[0], _woodDoorClips[0], true);
                break;
            case DoorType.metalDoor:
                PlaySound(_audioSource[0], _metalDoorClips[0], true);
                break;
            case DoorType.heavyDoor:
                PlaySound(_audioSource[0], _heavyDoorClips[0], true);
                break;
        }
    }

    private void PlaySound(AudioSource source, AudioClip clip, bool randomPitch)
    {
        if (randomPitch)
        {
            var randomFloat = Random.Range(-1f, .1f);
            source.pitch += randomFloat;
        }
        
        source.PlayOneShot(clip);

    }

    private void DoorOpen(AudioSource source, bool randomPitch)
    {
        source.enabled = true;

        if (randomPitch)
        {
            var randomFloat = Random.Range(-1f, .1f);
            source.pitch += randomFloat;
        }

        source.Play();
    }

    private void DoorStop(AudioSource source)
    {
        source.Stop();
    }
}
