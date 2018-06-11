using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollController : MonoBehaviour {

    public Component[] boneRig;
    public float hitForce = 5f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {

        boneRig = GetComponentsInChildren<Rigidbody>();
        rb = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KillRagdoll(Vector3 hitDirection, GameObject hitBodyPart)
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        //rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        foreach (Rigidbody ragdoll in boneRig)
        {
            //ragdoll.isKinematic = false;
            ragdoll.velocity = hitDirection * hitForce;// Vector3.zero;
        }

        //hitBodyPart.GetComponent<Rigidbody>().velocity = hitDirection * hitForce;

    }
}
