using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour {

    //public bool isUnlocked;
    public GameObject door;
    private HingeJoint hinge;
    public string doorToUnlock;
    private bool isUnlocked;
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
        if (other.tag == "Collectable" && doorToUnlock == other.GetComponent<Collectable>().itemName && !isUnlocked)
        {
            Unlock();
        }
    }

    

    public void Unlock()
    {
        isUnlocked = true;
        hinge.useLimits = false;
    }

}
