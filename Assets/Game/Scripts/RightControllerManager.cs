using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControllerManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public GameObject flashlight;
    public GameObject inventory;

    public GameObject collectable;
    public GameObject currentInHand; //current GameObject in players hand



    public bool inventoryOpen;
    public bool firstPressUp;

	// Use this for initialization
	void Start () {
        inventory.SetActive(false);
        if (currentInHand.gameObject == null)
        {
            flashlight.gameObject.SetActive(true);
            currentInHand = flashlight;
        }
    }
	
	// Update is called once per frame
	void Update () {
         device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !inventoryOpen)
        {
            HideFlashlight();
            //OpenInventory();
            inventoryOpen = true;
            inventory.SetActive(true);

            print("inventory show");
        }

        

        if (inventoryOpen)
        {
            OpenInventory();
        }





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
        flashlight.gameObject.SetActive(false);
    }

    public void ShowFlashlight()
    {
        flashlight.gameObject.SetActive(true);
    }

    public void OpenInventory()
    {
        //inventoryOpen = true;

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && firstPressUp)
        {
            ShowFlashlight();
            //CloseInventory();
            inventoryOpen = false;
            firstPressUp = false;
            print("inventory hide");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && !firstPressUp)
        {
            firstPressUp = true;
        }
    }

    public void CloseInventory()
    {
        inventoryOpen = false;
    }
}
