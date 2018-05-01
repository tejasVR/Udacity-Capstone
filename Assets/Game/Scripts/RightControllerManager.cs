﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControllerManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    //public GameObject flashlight;
    public GameObject inventory;
    public GameObject handModel;

    //public Transform handAttachPoint;

    //public GameObject collectable;
    //public GameObject currentInHand; //current GameObject in players hand

    public List<CollectableItem> itemList = new List<CollectableItem>(); //creates a list of items for the invebtory to manage

    public Vector2 touchpad;
    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public bool inventoryOpen;
    public bool firstPressUp;

    public bool hasItemInHand;

    public int currentItem;
    public int oldItem;

    public bool hasReel1;
    public bool hasReel2;
    public bool hasReel3;
    public bool hasBasementKey;
    public bool hasAtticKey;

    public GameObject objInHand;


    // Use this for initialization
    void Start () {
        inventory.SetActive(false);
        /*if (currentInHand.gameObject == null)
        {
            flashlight.gameObject.SetActive(true);
            currentInHand = flashlight;
        }*/
    }
	
	// Update is called once per frame
	void Update () {
         device = SteamVR_Controller.Input((int)trackedObj.index);
        

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !inventoryOpen)
        {
            //HideFlashlight();
            //OpenInventory();
            inventoryOpen = true;
            inventory.SetActive(true);
            CheckItems();
            handModel.gameObject.SetActive(true);

            if (objInHand != null)
            {
                objInHand.SetActive(false);
                hasItemInHand = false;
            }
            

            //print("inventory show");
        }

        

        if (inventoryOpen)
        {
            OpenInventory();
        }

        if (hasItemInHand)
        {
            objInHand.transform.position = Vector3.Lerp(objInHand.transform.position, transform.position, Time.deltaTime * 12f);
            objInHand.transform.rotation = Quaternion.Slerp(objInHand.transform.rotation, transform.rotation, Time.deltaTime * 12f);
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

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Collectable")
        {
            //print("I'm colliding with something!");
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                //print("trying to collect a collectable!");
                var collectable = collision.gameObject.GetComponent<Collectable>();
                foreach (CollectableItem item in itemList)
                {
                    item.hasItemInHand = false;

                    if (!item.hasItemInInventory)
                    {
                        if(collectable.itemName == item.name)
                        {
                            // We have the item in our inventory
                            item.hasItemInInventory = true;

                            // The item that will be in the player's hand is the item the player has just collected
                            item.itemInHandObj = collision.gameObject;
                            objInHand = collision.gameObject;

                            // a check to say that the item is in the hands of the player
                            item.hasItemInHand = true;
                            hasItemInHand = true;

                        }
                    }
                }

                collectable.isCollected = true;

                handModel.gameObject.SetActive(false);


            }
        }
       
    }

    /*
    public void HideFlashlight()
    {
        flashlight.gameObject.SetActive(false);
    }

    public void ShowFlashlight()
    {
        flashlight.gameObject.SetActive(true);
    }*/

    public void OpenInventory()
    {
        foreach(CollectableItem item in itemList)
        {
            //If the item object is embedded within the item list
            /*if (item.itemObj != null)
            {
                item.itemObj.transform.position = Vector3.Lerp(item.itemObj.transform.position, item.inventoryAttachPoint.transform.position, Time.deltaTime * 3f);
                item.itemObj.transform.rotation = Quaternion.Slerp(item.itemObj.transform.rotation, item.inventoryAttachPoint.transform.rotation, Time.deltaTime * 3f);

                item.itemObj.transform.localScale = Vector3.Lerp(item.itemObj.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 3f);
            }*/
        }

        // if the menu is open, get both the x and y values of the touchpad
        touchpad.x = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        touchpad.y = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        Vector2 fromVector2 = new Vector2(0, 1);
        Vector2 toVector2 = touchpad;

        // Measure the users' thumb angle in relation to the center of the touchpad.
        angleFromCenter = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        // This will get the angle of the users' thumb so we know what inventory item they are trying to highlight
        if (cross.z > 0)
        {
            angleFromCenter = 360 - angleFromCenter;
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
                //print("Reel #1");

            }

            // Reel #2
            if (angleFromCenter > 306 && angleFromCenter < 342)
            {
                currentItem = 1;
                //print("Reel #2");
            }

            // Reel #3
            if (angleFromCenter > 342 || angleFromCenter < 18)
            {
                currentItem = 2;
                //print("Reel #3");
            }

            // Basement Key
            if (angleFromCenter > 18 && angleFromCenter < 54)
            {
                currentItem = 3;
                //print("Basement Key");
            }

            // Attic Key
            if (angleFromCenter > 54 && angleFromCenter < 90)
            {
                currentItem = 4;

                /*if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && firstPressUp)
                {
                    
                    objInHand = itemList[currentItem].itemInHandObj;
                    objInHand.SetActive(true);
                    hasItemInHand = true;
                    CloseInventory();

                }*/

                //print("Attic Key");
            }
        } else
        {
            currentItem = -1;
        }

        if (currentItem != oldItem)
        {
            oldItem = currentItem;
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && currentItem > -1 && firstPressUp)
        {
            if (itemList[currentItem].itemInHandObj != null)
            {
                objInHand = itemList[currentItem].itemInHandObj;
                objInHand.SetActive(true);
                hasItemInHand = true;
            }
            

           
            CloseInventory();

            //HoldInHand(itemList[currentItem].itemObj);
            //print(currentItem);
            // itemList[currentItem].itemObj.GetComponent<Collectible>().isPickedUp = true;
            // itemList[currentItem].itemObj.GetComponent<Collectible>().isSentFromHand = true;
            //itemList[currentItem].hasItemInInventory = false;
            //itemList[currentItem].inventoryTtemObj.transform.parent = null;
            //itemList[currentItem].inventoryTtemObj.GetComponent<BoxCollider>().enabled = true;

            //itemList[currentItem].inventoryTtemObj = null;

        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && firstPressUp & currentItem == -1)
        {
            //ShowFlashlight();
            CloseInventory();
            //inventoryOpen = false;
            
            //print("inventory hide");

            // Check what items the player currently has
            //CheckItems();
        }







    }

    public void CloseInventory()
    {
        inventoryOpen = false;
        inventory.SetActive(false);

        firstPressUp = false;
    }

    // Function to collect an item and track it in the players inventory
    public void CollectItem(string itemName, GameObject itemObject)
    {
        // For the items that have not yet been collected
        foreach(CollectableItem item in itemList)
        {
            if (!item.hasItemInInventory)
            {
                // If the name of the collected item is the same as the one marked in the inventory, collect that specific item
                if(item.name == itemName)
                {
                    // Check the item as being in the inventory
                    item.hasItemInInventory = true;
                    //item.itemObj = itemObject;
                    //item.itemObj.transform.parent = item.inventoryAttachPoint.transform;
                }

                
            }
        }
    }

    void CheckItems() //check what items the player currently has
    {
        foreach(CollectableItem item in itemList)
        {
            if (item.hasItemInInventory)
            {
                item.inventoryObj.SetActive(true);
            } else
            {
                item.inventoryObj.SetActive(false);
            }
        }
    }

    /*public void HoldInHand(GameObject itemObj)
    {
        itemObj.transform.position = Vector3.Lerp(itemObj.transform.position, attachPoint.transform.position, Time.deltaTime * 3f);
        itemObj.transform.rotation = Quaternion.Lerp(itemObj.transform.rotation, attachPoint.transform.rotation, Time.deltaTime * 3f);
    }*/

    [System.Serializable]
    public class CollectableItem
    {
        public string name;
        public GameObject inventoryObj; // the whole of the individual inventory UI
        public GameObject inventoryTtemObj; // just the item object that is in the inventory
        public GameObject itemInHandObj; // the item that will be in the hands of the player
        //public bool 
        //public GameObject inventoryAttachPoint;
        public bool hasItemInInventory;
        public bool hasItemInHand;
    }

}
