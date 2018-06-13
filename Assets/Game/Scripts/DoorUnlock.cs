﻿using System.Collections;
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

    private AudioSource _audioSource;
    public AudioClip[] _unlockSounds;

    //public string 

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        _hinge = _door.GetComponent<HingeJoint>();
        _hinge.useLimits = true;
	}
	
    private void OnTriggerEnter(Collider other)
    {
        //if ((other.CompareTag("Collectable") || other.CompareTag("Collected")) && _keyToUnlock == other.GetComponent<Collectable>().itemName && !_isUnlocked)
        if (other.CompareTag("Collected") && _keyToUnlock == other.GetComponent<Collectable>().itemName && !_isUnlocked)
        {
            Unlock();
            _rightControllerManager.AttachToDoor(_keyToUnlock, _keyAttach);
            //AttachToDoor(other.gameObject, other.GetComponent<Collectable>().itemName);
        }
    }

    public void Unlock()
    {
        PlaySound.PlayAudioFromSelection(_audioSource, _unlockSounds, true, -.05f, .05f);
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
