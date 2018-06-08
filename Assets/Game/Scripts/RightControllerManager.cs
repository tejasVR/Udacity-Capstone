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

    public Color idleUIColor;
    public Color highlightedUIColor;

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
                    if (item.itemInHandObj != null)
                    {
                        item.itemInHandObj.SetActive(false);
                    }

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
                collectable.isCollected = true;
                handModel.gameObject.SetActive(false);

                // The item that will be in the player's hand is the item the player has just collected
                //objInHand = collision.gameObject;
                collision.gameObject.transform.parent = this.transform;

                collision.gameObject.transform.localPosition = collectable.attachPoint.localPosition;
                collision.gameObject.transform.localRotation = collectable.attachPoint.localRotation;

                //collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;


                // a check to say that the item is in the hands of the player
                //item.hasItemInHand = true;
                hasItemInHand = true;





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
            // Basement Key
            if (angleFromCenter > 220 && angleFromCenter < 280)
            {
                currentItem = 0;
                
            }
            // Front Door Key
            else if (angleFromCenter > 290 && angleFromCenter < 350)
            {
                currentItem = 1;
                
            }
            // Pistol
            else if (angleFromCenter > 10 && angleFromCenter < 70)
            {
                currentItem = 2;
                
            }
            // Attic Key
            else if (angleFromCenter > 80 && angleFromCenter < 140)
            {
                currentItem = 3;

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
                    item.inventoryObj.GetComponent<Image>().color = idleUIColor;
                }

                itemList[currentItem].inventoryObj.GetComponent<Image>().color = highlightedUIColor;
                //itemList[oldItem].inventoryObj.GetComponent<Image>().color = Color.white;
            } else
            {
                foreach (var item in itemList)
                {
                    item.inventoryObj.GetComponent<Image>().color = idleUIColor;
                }
            }

            oldItem = currentItem;
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && currentItem > -1 && firstPressUp)
        {
            if (itemList[currentItem].itemInHandObj != null)
            {
                itemList[currentItem].itemInHandObj.SetActive(true);
                handModel.SetActive(false);
                //objInHand.transform.position = transform.position;
                //objInHand = itemList[currentItem].itemInHandObj;
                //objInHand.SetActive(true);
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
                item.inventoryItemObj.SetActive(true);
                item.itemInHandObj.SetActive(false);
            }
            else if(!item.hasItemInInventory)
            {
                item.inventoryItemObj.SetActive(false);
            }

        }
    }

    public void GiveAwayItem(string itemName)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].name == itemName)
            {
                itemList[i].hasItemInInventory = false;
                itemList[i].hasItemInHand = false;
            }
        }

        handModel.SetActive(true);
    }

   

    [System.Serializable]
    public class CollectableItem
    {
        public string name;
        public GameObject inventoryObj; // the whole of the individual inventory UI
        public GameObject inventoryItemObj; // just the item object that is in the inventory
        public GameObject itemInHandObj; // the item that will be in the hands of the player
        public GameObject textTag;
        public bool hasItemInInventory;
        public bool hasItemInHand;
    }

}
