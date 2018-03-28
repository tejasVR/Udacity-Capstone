using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour {

    public GameObject explosionForcePoint;
    public float explosionRadius;
    public float explosionPower;
    public float explosionPowerUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoExplosionForce()
    {
        //Rigidbody rb = explosionForcePoint.GetComponent<Rigidbody>();
        Vector3 explosionPos = explosionForcePoint.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, explosionPowerUp);
            }
        }

        //print("Did explosion");

        this.gameObject.SetActive(false);
    }
}
