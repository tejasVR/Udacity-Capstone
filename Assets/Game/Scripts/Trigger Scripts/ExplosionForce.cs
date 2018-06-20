using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour {

    //public GameObject explosionForcePoint;
    public float explosionRadius = .5f;
    public float explosionPower = 100f;
    public float explosionPowerUp = 100f;

    public AudioSource _audioSource;

    public bool playAudio;

	// Use this for initialization
	void Start () {
        //_audioSource = GetComponent<AudioSource>();
	}

    public void DoExplosionForce()
    {
        //Rigidbody rb = explosionForcePoint.GetComponent<Rigidbody>();
        //Vector3 explosionPos = explosionForcePoint.transform.position;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, transform.position, explosionRadius, explosionPowerUp);
            }
        }

        if (playAudio)
            PlaySound.PlayAudio(_audioSource);

        //print("Did explosion");

        //this.gameObject.SetActive(false);
    }
}
