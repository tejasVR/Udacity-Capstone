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
    public AudioClip[] _soundsWhenEnabled;

    public Light[] _lights;

    public string itemName;

    BoxCollider boxCollider;

    public Transform attachPoint;

    private bool _firstPickUp;

    void Start () {
        //_light = GetComponent<Light>();
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

        if (isCollected && !_firstPickUp)
        {
                for (int i = 0; i < rend.Length; i++)
                {
                    rend[i].material = matNormal[i];
                }

            this.gameObject.layer = 13;

            _firstPickUp = true;
                //if (itemName != "Pistol")
                //    _light.enabled = true;
            
        }

        if (gameObject.CompareTag("Collected"))
        {
            if (itemName != "Pistol")
            {
                foreach (var light in _lights)
                {
                    light.enabled = true;
                }
            }
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    print(collision.gameObject.tag);

    //    if (collision.gameObject.CompareTag("Controller") && !isCollected)
    //    {
    //        for(int i = 0; i < rend.Length; i++)
    //        {
    //            rend[i].material = onHoverMat;
    //        }

    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        //print(other.gameObject.tag);

        if (other.gameObject.CompareTag("Controller") && !isCollected)
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material = onHoverMat;
            }

        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Controller") && !isCollected)
    //    {
    //        for (int i = 0; i < rend.Length; i++)
    //        {
    //            rend[i].material = matNormal[i];
    //        }
    //    }      
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Controller") && !isCollected)
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material = matNormal[i];
            }
        }
    }
}
