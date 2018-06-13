using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject original;
    public GameObject destroyedVersion;
    private BoxCollider boxCollider;

    private AudioSource _audioSource;
    public AudioClip[] _hitClips;
    public AudioClip[] _destroyClips;

    public int health = 3;

    //public float explosionForce;

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
	}


    //private void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Collected"))
    //    {
    //        var otherRb = col.gameObject.GetComponent<Rigidbody>();

    //        print(otherRb.angularVelocity.magnitude);

    //        if (otherRb.angularVelocity.magnitude > .01f)
    //        {
    //            print("destructible was hit");
    //            PlaySound.PlayAudioFromSelection(_audioSource, _hitClips, true, -.05f, .05f);
    //            health--;

    //            if (health <= 0)
    //                DestroyIntoPieces();
    //        }
    //    }


    //}

    //private void OnCollisionEnter(Collision col)
    //{
    //    print("I've collided with something!");
    //    //if (col.gameObject.CompareTag("Collected"))
    //    //if (col.gameObject.tag == "Collected")
    //    {
    //        //print("I've collided with a Collected object!");

    //        //var otherRb = col.gameObject.GetComponent<Rigidbody>();
    //        print(col.gameObject);
    //        var gun = col.gameObject.GetComponent<GunScript>();

    //        if (gun != null)
    //        {
    //            print(gun.deviceAngularVelocity);

    //            if (gun.deviceAngularVelocity > 3f)
    //            {
    //                print("destructible was hit");
    //                PlaySound.PlayAudioFromSelection(_audioSource, _hitClips, true, -.05f, .05f);
    //                health--;

    //                if (health <= 0)
    //                    DestroyIntoPieces();
    //            }
    //        }
    //    }
    //}

    public void HitPiece()
    {
        PlaySound.PlayAudioFromSelection(_audioSource, _hitClips, true, -.05f, .05f);

        health--;

        if (health <= 0)
            DestroyIntoPieces();

    }

    public void DestroyIntoPieces()
    {
        //StackTrace stackTrace = new StackTrace();
        //UnityEngine.Debug.Log(stackTrace.GetFrame(1).GetMethod().Name);
        boxCollider.enabled = false;
        destroyedVersion.SetActive(true);
        original.SetActive(false);

        PlaySound.PlayAudioFromSelection(_audioSource, _destroyClips, true, -.05f, .05f);
    }

    
}
