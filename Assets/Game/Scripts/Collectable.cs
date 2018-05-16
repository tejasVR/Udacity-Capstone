using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public Material normalMat;
    public Material onHoverMat;

    public bool isCollected;

    Renderer rend;

    public string itemName;

    BoxCollider boxCollider;

    void Start () {

        rend = GetComponentInChildren<Renderer>();
        boxCollider = GetComponent<BoxCollider>();
        normalMat = rend.material;

	}
	
	void Update () {

        if (isCollected)
        {
            if (!boxCollider.isTrigger)
                boxCollider.isTrigger = true;

            if (rend.material != normalMat)
                rend.material = normalMat;

            
        }
    }

    private void FixedUpdate()
    {
        //if (isCollected)
        {
            //transform.position = trackedObj.transform.position;
            //transform.rotation = trackedObj.transform.rotation;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Controller" && !isCollected)
        {
            rend.material = onHoverMat;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Controller" && !isCollected)
        {
            rend.material = normalMat;
        }      
    }
}
