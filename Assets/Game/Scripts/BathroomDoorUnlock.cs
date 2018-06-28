using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoorUnlock : MonoBehaviour {

    public GameObject _door;
    private HingeJoint _hinge;
    private Rigidbody _doorRb;

    // Use this for initialization
    void Awake () {
        _doorRb = _door.GetComponent<Rigidbody>();
        _hinge = _door.GetComponent<HingeJoint>();
        _hinge.useLimits = true;
        _doorRb.useGravity = false;
        _doorRb.isKinematic = true;
    }

    private void OnEnable()
    {
        _hinge.useLimits = false;
        _doorRb.useGravity = true;
        _doorRb.isKinematic = false;
    }
}
