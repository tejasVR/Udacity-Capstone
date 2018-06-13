using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour {

    //public bool isUnlocked;
    public GameObject _door;
    private HingeJoint _hinge;
    public string _keyToUnlock;
    private bool _isUnlocked;

    public Transform _keyAttach;
    public RightControllerManager _rightControllerManager;

    //public string 

	// Use this for initialization
	void Start () {

        _hinge = _door.GetComponent<HingeJoint>();
        _hinge.useLimits = true;
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable" && _keyToUnlock == other.GetComponent<Collectable>().itemName && !_isUnlocked)
        {
            Unlock();
            _rightControllerManager.AttachToDoor(_keyToUnlock, _keyAttach);
            //AttachToDoor(other.gameObject, other.GetComponent<Collectable>().itemName);
        }
    }

    public void Unlock()
    {
        _isUnlocked = true;
        _hinge.useLimits = false;
    }

    //public void AttachToDoor(GameObject key, string keyName)
    //{
    //    key.transform.position = _keyAttach.position;
    //    key.transform.parent = _keyAttach.transform;

    //    //rightControllerManager.GiveAwayItem(keyName);
    //}

}
