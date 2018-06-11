using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour {

    //public bool isUnlocked;
    public GameObject door;
    private HingeJoint hinge;
    public string doorToUnlock;
    private bool isUnlocked;

    public Transform keyAttach;
    RightControllerManager rightControllerManager;

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
            AttachToDoor(other.gameObject, other.GetComponent<Collectable>().itemName);
        }
    }

    

    public void Unlock()
    {
        isUnlocked = true;
        hinge.useLimits = false;
    }

    public void AttachToDoor(GameObject key, string keyName)
    {
        key.transform.position = keyAttach.position;
        key.transform.parent = keyAttach.transform;

        rightControllerManager.GiveAwayItem(keyName);
    }

}
