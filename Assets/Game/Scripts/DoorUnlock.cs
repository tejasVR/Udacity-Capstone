using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour {

    //public bool isUnlocked;
    public GameObject door;
    public HingeJoint hinge;
    public string doorToUnlock;
    //public string 

	// Use this for initialization
	void Start () {

    
        hinge = door.GetComponent<HingeJoint>();
        
        
        hinge.useLimits = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable" && doorToUnlock == other.GetComponent<Collectable>().itemName)
        {
            Unlock();
        }
    }

    

    public void Unlock()
    {
        hinge.useLimits = false;
    }

}
