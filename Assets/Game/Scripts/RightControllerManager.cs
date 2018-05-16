using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightControllerManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public GameObject inventory;
    public GameObject handModel;

    public List<CollectableItem> itemList = new List<CollectableItem>(); //creates a list of items for the invebtory to manage

    public Vector2 touchpad;
    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public bool inventoryOpen;
    public bool firstPressUp;

    public bool hasItemInHand;

    public int currentItem;
    public int oldItem;

    public GameObject objInHand;

    public GameObject cursor;


    void Start () {
        inventory.SetActive(false);
        
    }
	
	void Update () {
        if (trackedObj.gameObject.activeInHierarchy)
        {
            device = SteamVR_Controller.Input((int)trackedObj.index);
        }


        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !inventoryOpen)
        {
            
            inventoryOpen = true;
            inventory.SetActive(true);
            CheckItems();
            handModel.gameObject.SetActive(true);

            if (objInHand != null)
            {
                objInHand.SetActive(false);
                hasItemInHand = false;
            }
            
        }

        if (inventoryOpen)
        {
            OpenInventory();
        }

        if (hasItemInHand)
        {
            //objInHand.transform.position = Vector3.Lerp(objInHand.transform.position, transform.position, Time.deltaTime * 12f);
            //objInHand.transform.rotation = Quaternion.Slerp(objInHand.transform.rotation, transform.rotation, Time.deltaTime * 12f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Collectable")
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
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
                            item.itemInHandObj = collision.gameObject;


                        }
                    }
                }

                // The item that will be in the player's hand is the item the player has just collected
                //objInHand = collision.gameObject;
                collision.gameObject.transform.parent = this.transform;
                //collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;


                // a check to say that the item is in the hands of the player
                //item.hasItemInHand = true;
                hasItemInHand = true;


                collectable.isCollected = true;

                handModel.gameObject.SetActive(false);


            }
        }
       
    }
    public void OpenInventory()
    {
        cursor.SetActive(true);

        // if the menu is open, get both the x and y values of the touchpad
        touchpad.x = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        touchpad.y = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        cursor.transform.localPosition = Vector3.Lerp(cursor.transform.localPosition, touchpad * .085f, Time.unscaledDeltaTime * 10f);

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
            if (angleFromCenter > 250 && angleFromCenter < 290)
            {
                currentItem = 0;
                //print("Reel #1");
            }
            else if (angleFromCenter > 340 || angleFromCenter < 20)
            {
                currentItem = 1;
                //print("Reel #3");
            } else if (angleFromCenter > 70 && angleFromCenter < 110)
            {
                currentItem = 2;
                //print("Basement Key");
            }
            else
            {
                currentItem = -1;
            }
        }
        else
        {
            currentItem = -1;
        }




        if (currentItem != oldItem)
        {
            if (currentItem > -1)
            {
                foreach (var item in itemList)
                {
                    item.inventoryObj.GetComponent<Image>().color = Color.white;
                }

                itemList[currentItem].inventoryObj.GetComponent<Image>().color = Color.yellow;
                //itemList[oldItem].inventoryObj.GetComponent<Image>().color = Color.white;
            } else
            {
                foreach (var item in itemList)
                {
                    item.inventoryObj.GetComponent<Image>().color = Color.white;
                }
            }

            oldItem = currentItem;
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && currentItem > -1 && firstPressUp)
        {
            if (itemList[currentItem].itemInHandObj != null)
            {
                //objInHand.transform.position = transform.position;
                objInHand = itemList[currentItem].itemInHandObj;
                objInHand.SetActive(true);
                hasItemInHand = true;
            }
            

           
            CloseInventory();

          

        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && firstPressUp & currentItem == -1)
        {
            CloseInventory();
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
                item.inventoryTtemObj.SetActive(true);
            } else
            {
                item.inventoryTtemObj.SetActive(false);
            }
        }
    }

   

    [System.Serializable]
    public class CollectableItem
    {
        public string name;
        public GameObject inventoryObj; // the whole of the individual inventory UI
        public GameObject inventoryTtemObj; // just the item object that is in the inventory
        public GameObject itemInHandObj; // the item that will be in the hands of the player
       
        public bool hasItemInInventory;
        public bool hasItemInHand;
    }

}
