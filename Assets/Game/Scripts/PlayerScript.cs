using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject _playerEyeObj;
    public GameObject _cameraRigObj;
    public GameObject _bodyColliderObj;
    public SteamVR_TrackedObject _trackedRightObj;
    public SteamVR_TrackedObject _trackedLeftObj;

    public static GameObject _playerEye; //player eye
    public static GameObject _cameraRig; //player eye
    public static Rigidbody _cameraRigRb; //player eye

    public static GameObject _bodyCollider;

    public static SteamVR_TrackedObject _trackedRight;
    public static SteamVR_TrackedObject _trackedLeft;

    public static SteamVR_Controller.Device _deviceRight;
    public static SteamVR_Controller.Device _deviceLeft;

    public static float _triggerAxisRight;
    public static float _triggerAxisLeft;

    public static Vector2 _touchpadRight;

    // Use this for initialization
    void Start () {
        _playerEye = _playerEyeObj;
        _cameraRig = _cameraRigObj;
        _bodyCollider = _bodyColliderObj;
        _cameraRigRb = _cameraRig.GetComponent<Rigidbody>();

        _trackedLeft = _trackedLeftObj;
        _trackedRight = _trackedRightObj;

        _deviceRight = SteamVR_Controller.Input((int)_trackedRightObj.index);
        _deviceLeft = SteamVR_Controller.Input((int)_trackedLeftObj.index);
    }
	
	// Update is called once per frame
	void Update () {

        if (_trackedRightObj.gameObject.activeInHierarchy)
        {
            _deviceRight = SteamVR_Controller.Input((int)_trackedRightObj.index);
            _triggerAxisRight = _deviceRight.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x; //Gets depth of trigger press

            _touchpadRight.x = _deviceRight.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            _touchpadRight.y = _deviceRight.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        }
        if (_trackedLeftObj.gameObject.activeInHierarchy)
        {
            _deviceLeft = SteamVR_Controller.Input((int)_trackedLeftObj.index);
            _triggerAxisLeft = _deviceLeft.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x; //Gets depth of trigger press    

        }


        //print(_triggerAxisRight);
    }
}
