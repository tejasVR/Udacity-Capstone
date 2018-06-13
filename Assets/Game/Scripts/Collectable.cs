using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    //public SteamVR_TrackedObject trackedObj;

    //public Material normalMat;
    public Material onHoverMat;

    public bool isCollected;

    public Renderer[] rend;
    Material[] matNormal;
    

    public string itemName;

    BoxCollider boxCollider;

    public Transform attachPoint;

    void Start () {

        matNormal = new Material[rend.Length];

        for(int i = 0; i < rend.Length; i++)
        {
            matNormal[i] = rend[i].material;
        }
        //rend = GetComponentInChildren<Renderer>();
        boxCollider = GetComponent<BoxCollider>();
        //normalMat = rend.material;

	}
	
	void Update () {

        if (isCollected)
        {
            if (!boxCollider.isTrigger)
                boxCollider.isTrigger = true;

            for(int i = 0; i < rend.Length; i++)
            {
                rend[i].material = matNormal[i];
            }

            
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
        if (collision.gameObject.CompareTag("Controller") && !isCollected)
        {
            for(int i = 0; i < rend.Length; i++)
            {
                rend[i].material = onHoverMat;
            }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Controller") && !isCollected)
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material = matNormal[i];
            }
        }      
    }
}
