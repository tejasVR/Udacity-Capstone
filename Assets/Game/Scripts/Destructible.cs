using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject original;
    public GameObject destroyedVersion;
    private BoxCollider boxCollider;

    private AudioSource _audioSource;
    public AudioClip[] _clips;

    //public float explosionForce;

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        
        var otherRb = col.gameObject.GetComponent<Rigidbody>();

        //print(otherRb.angularVelocity.magnitude);

        if (otherRb.angularVelocity.magnitude > .15f)
        {
            DestroyIntoPieces();
        }

    }

    

    public void DestroyIntoPieces()
    {
        boxCollider.enabled = false;
        destroyedVersion.SetActive(true);

        original.SetActive(false);

        PlaySound.PlayAudioFromSelection(_audioSource, _clips, true);
    }

    
}
