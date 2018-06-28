using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollController : MonoBehaviour {

    public Component[] boneRig;
    public GameObject[] _wrists;
    public float hitForce = 5f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {

        boneRig = GetComponentsInChildren<Rigidbody>();
        rb = GetComponent<Rigidbody>();
		
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
            //if (ragdoll.GetComponent<Rigidbody>() != null)
            ragdoll.velocity = hitDirection * hitForce;// Vector3.zero;
            ragdoll.gameObject.layer = 15;
            //ragdoll.isKinematic = true;
        }

        foreach (GameObject obj in _wrists)
        {
            obj.gameObject.layer = 15;
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Rigidbody>().useGravity = false;
        }

        //foreach (Component obj in boneRig)
        //{
        //    obj.gameObject.layer = 15;
        //}

        //hitBodyPart.GetComponent<Rigidbody>().velocity = hitDirection * hitForce;

        //var children = new GameObject[transform.childCount];
        //var children = GetComponentInChildren<Transform>();
        //foreach (Transform child in children)
        //{
        //    print("I am cycling through the child objects. Current child:" + child.gameObject.name);
        //    child.gameObject.layer = 15;
        //}

    }
}
