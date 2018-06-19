using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedback : MonoBehaviour {

    //SteamVR_TrackedObject _trackedObj;
    //static SteamVR_Controller.Device _device;

	// Use this for initialization
	void Start () {
        //_trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (_trackedObj.gameObject.activeInHierarchy)
        {
        //    _device = SteamVR_Controller.Input((int)_trackedObj.index);
        }
    }

    public static void HapticAmount(int hapticAmount, bool rightController, bool leftController)
    {
        if (rightController)
            PlayerScript._deviceRight.TriggerHapticPulse((ushort)hapticAmount);

        if (leftController)
            PlayerScript._deviceLeft.TriggerHapticPulse((ushort)hapticAmount);
    }


}
