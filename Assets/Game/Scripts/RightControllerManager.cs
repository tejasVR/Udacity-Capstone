using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControllerManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public GameObject flashLight;
    public GameObject collectable;
    public GameObject currentInHand; //current GameObject in players hand

	// Use this for initialization
	void Start () {
        if (currentInHand.gameObject == null)
        {
            flashLight.gameObject.SetActive(true);
            currentInHand = flashLight;
        }
    }
	
	// Update is called once per frame
	void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        /*
        if (currentInHand.tag != "Flashlight" && currentInHand != null)
        {
            //flashLight.gameObject.SetActive(false);
        } else
        {
            //flashLight.gameObject.SetActive(true);
            currentInHand = flashLight;
        }

        if (currentInHand == null)
        {
            //flashLight.gameObject.SetActive(true);
            currentInHand = flashLight;
        }
		*/
	}

    public void HideFlashlight()
    {
        flashLight.gameObject.SetActive(false);
    }

    public void ShowFlashlight()
    {
        flashLight.gameObject.SetActive(true);
    }
}
