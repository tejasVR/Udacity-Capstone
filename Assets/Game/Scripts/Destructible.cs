using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject original;
    public GameObject destroyedVersion;
    public BoxCollider boxCollider;

    public float explosionForce;

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestroyIntoPieces()
    {
        boxCollider.enabled = false;
        destroyedVersion.SetActive(true);

        original.SetActive(false);
    }
}
