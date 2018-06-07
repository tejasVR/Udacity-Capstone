using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour {

    public Component[] boneRig;
    public float hitForce = 5f;

	// Use this for initialization
	void Start () {

        boneRig = GetComponentsInChildren<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KillRagdoll(Vector3 hitDirection, GameObject hitBodyPart)
    {
        foreach (Rigidbody ragdoll in boneRig)
        {
            ragdoll.isKinematic = false;
            ragdoll.velocity = Vector3.zero;
        }

        hitBodyPart.GetComponent<Rigidbody>().velocity = hitDirection * hitForce;

        GetComponent<Animator>().enabled = false;
    }
}
