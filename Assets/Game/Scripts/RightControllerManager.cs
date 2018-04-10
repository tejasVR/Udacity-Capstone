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

    public List<CollectableItem> itemList = new List<CollectableItem>(); //creates a list of items for the invebtory to manage

    public Vector2 touchpad;
    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public bool inventoryOpen;
    public bool firstPressUp;

    public int currentItem;
    public int oldItem;

    public bool hasReel1;
    public bool hasReel2;
    public bool hasReel3;
    public bool hasBasementKey;
    public bool hasAtticKey;


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
            CheckItems();
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
        touchpad.x = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        touchpad.y = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        Vector2 fromVector2 = new Vector2(0, 1);
        Vector2 toVector2 = touchpad;

        angleFromCenter = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        if (cross.z > 0)
        {
            angleFromCenter = 360 - angleFromCenter;
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && firstPressUp)
        {
            ShowFlashlight();
            //CloseInventory();
            inventoryOpen = false;
            inventory.SetActive(false);

            firstPressUp = false;
            //print("inventory hide");

            // Check what items the player currently has
            CheckItems();
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && !firstPressUp)
        {
            firstPressUp = true;
        }

        if (touchpad.magnitude > .25f)
        {
            // Reel #1
            if (angleFromCenter > 270 && angleFromCenter < 306)
            {
                currentItem = 0;
                print("Reel #1");

            }

            // Reel #2
            if (angleFromCenter > 306 && angleFromCenter < 342)
            {
                currentItem = 1;
                print("Reel #2");
            }

            // Reel #3
            if (angleFromCenter > 342 || angleFromCenter < 18)
            {
                currentItem = 2;
                print("Reel #3");
            }

            // Basement Key
            if (angleFromCenter > 18 && angleFromCenter < 54)
            {
                currentItem = 3;
                print("Basement Key");
            }

            // Attic Key
            if (angleFromCenter > 54 && angleFromCenter < 90)
            {
                currentItem = 4;
                print("Attic Key");
            }
        }

        if (currentItem != oldItem)
        {
            oldItem = currentItem;
        }

        
       
        

        

        
    }

    public void CloseInventory()
    {
        inventoryOpen = false;
    }

    public void CollectItem(string itemName, GameObject itemObject)
    {
        foreach(CollectableItem item in itemList)
        {
            if (!item.hasItem)
            {
                if(item.name == itemName)
                {
                    item.hasItem = true;
                    item.itemObj = itemObject;
                    item.itemObj.transform.parent = item.attachPoint.transform;
                }

                
            }
        }
    }

    void CheckItems() //check what items the player currently has
    {
        foreach(CollectableItem item in itemList)
        {
            if (item.hasItem)
            {
                item.inventoryObj.SetActive(true);
            } else
            {
                item.inventoryObj.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class CollectableItem
    {
        public string name;
        public GameObject inventoryObj;
        public GameObject itemObj;
        public GameObject attachPoint;
        public bool hasItem;
    }

}
