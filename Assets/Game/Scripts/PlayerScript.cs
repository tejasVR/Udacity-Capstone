﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject _playerEyeObj;
    public GameObject _cameraRigObj;
    public GameObject _bodyColliderObj;
    public SteamVR_TrackedObject _trackedRightObj;
    public SteamVR_TrackedObject _trackedLeftObj;

    public GameObject _dominantHandObjects;
    public GameObject _nonDominantHandObjects;

    public static GameObject _playerEye; //player eye
    public static GameObject _cameraRig; //player eye
    public static Rigidbody _cameraRigRb; //player eye

    public static GameObject _bodyCollider;

    public static SteamVR_TrackedObject _dominantHand;
    public static SteamVR_TrackedObject _nonDominnatHand;

    public static SteamVR_Controller.Device _deviceDominant;
    public static SteamVR_Controller.Device _deviceNonDominant;

    public static float _triggerAxisDominant;
    public static float _triggerAxisNonDominant;

    public static Vector2 _touchpadDominant;
    public static Vector2 _touchpadNonDominant;

    public static bool _rightDominant = true;

    // Use this for initialization
    private void Awake()
    {
        //_playerEye = _playerEyeObj;
        //_cameraRig = _cameraRigObj;
        //_bodyCollider = _bodyColliderObj;
        //_cameraRigRb = _cameraRig.GetComponent<Rigidbody>();

        //_trackedLeft = _trackedLeftObj;
        //_trackedRight = _trackedRightObj;

        //_deviceRight = SteamVR_Controller.Input((int)_trackedRightObj.index);
        //_deviceLeft = SteamVR_Controller.Input((int)_trackedLeftObj.index);
    }

    void Start () {
        _playerEye = _playerEyeObj;
        _cameraRig = _cameraRigObj;
        _bodyCollider = _bodyColliderObj;
        _cameraRigRb = _cameraRig.GetComponent<Rigidbody>();

        _dominantHand = _trackedRightObj;
        _nonDominnatHand = _trackedLeftObj;

        _deviceDominant = SteamVR_Controller.Input((int)_dominantHand.index);
        _deviceNonDominant = SteamVR_Controller.Input((int)_nonDominnatHand.index);
    }
	
	// Update is called once per frame
	void Update () {

        if (_dominantHand.gameObject.activeInHierarchy)
        {
            _deviceDominant = SteamVR_Controller.Input((int)_dominantHand.index);
            _triggerAxisDominant = _deviceDominant.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x; //Gets depth of trigger press

            _touchpadDominant.x = _deviceDominant.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            _touchpadDominant.y = _deviceDominant.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        }
        if (_nonDominnatHand.gameObject.activeInHierarchy)
        {
            _deviceNonDominant = SteamVR_Controller.Input((int)_nonDominnatHand.index);
            _triggerAxisNonDominant = _deviceNonDominant.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x; //Gets depth of trigger press

            _touchpadNonDominant.x = _deviceNonDominant.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            _touchpadNonDominant.y = _deviceNonDominant.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        }


        //print(_triggerAxisRight);
    }

    public void SwitchHands()
    {
        if (_dominantHand == _trackedRightObj)
        {
            _dominantHand = _trackedLeftObj;
            _nonDominnatHand = _trackedRightObj;

            _dominantHandObjects.transform.parent = _trackedLeftObj.transform;
            _dominantHandObjects.transform.position = _trackedLeftObj.transform.position;
            _dominantHandObjects.transform.rotation = _trackedLeftObj.transform.rotation;
           
            _nonDominantHandObjects.transform.parent = _trackedRightObj.transform;
            _nonDominantHandObjects.transform.position = _trackedRightObj.transform.position;
            _nonDominantHandObjects.transform.rotation = _trackedRightObj.transform.rotation;

            _rightDominant = false;

        } else
        {
            _dominantHand = _trackedRightObj;
            _nonDominnatHand = _trackedLeftObj;

            _dominantHandObjects.transform.parent = _trackedRightObj.transform;
            _dominantHandObjects.transform.position = _trackedRightObj.transform.position;
            _dominantHandObjects.transform.rotation = _trackedRightObj.transform.rotation;

            _nonDominantHandObjects.transform.parent = _trackedLeftObj.transform;
            _nonDominantHandObjects.transform.position = _trackedLeftObj.transform.position;
            _nonDominantHandObjects.transform.rotation = _trackedLeftObj.transform.rotation;

            _rightDominant = true;


        }

    }
}
