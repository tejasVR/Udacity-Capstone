using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject original;
    public GameObject destroyedVersion;

    public float explosionForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestroyIntoPieces()
    {
        destroyedVersion.SetActive(true);

        original.SetActive(false);
    }
}
