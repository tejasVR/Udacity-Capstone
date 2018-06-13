using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjOnTrigger : MonoBehaviour {

    public RightControllerManager rightControllerManager;

    public GameObject[] objsToEnable;
    //public GameObject objToEnableWithBasementKey;
    //public GameObject objToEnableWithGun;



    public bool disableColliderOnTrigger;

    public enum HasItem
    {
        none,
        basementKey,
        pistol,
        frontDoorKey

    }

    public HasItem hasItem;

    private void Start()
    {
        foreach (var obj in objsToEnable)
        {
            obj.SetActive(false);
        }
        
        //objToEnableWithBasementKey.SetActive(false);
        //objToEnableWithGun.SetActive(false);

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasItem == HasItem.none)
                EnableObjects();

            if (hasItem == HasItem.frontDoorKey)
            {
                foreach(RightControllerManager.InventorySlot item in rightControllerManager._inventorySlots)
                {
                    if (item.name == "Front Door Key")
                    {
                        //if (item.hasItemInInventory)
                        {
                            EnableObjects();
                        }
                    }
                }
            }

            if (hasItem == HasItem.pistol)
            {
                foreach (RightControllerManager.InventorySlot item in rightControllerManager._inventorySlots)
                {
                    if (item.name == "Pistol")
                    {
                        //if (item.hasItemInInventory)
                        {
                            EnableObjects();
                        }
                    }
                }
            }

            if (hasItem == HasItem.basementKey)
            {
                foreach (RightControllerManager.InventorySlot item in rightControllerManager._inventorySlots)
                {
                    if (item.name == "Basement Key")
                    {
                        //if (item.hasItemInInventory)
                        {
                            EnableObjects();
                        }
                    }
                }
            }

            //if (ifHasBasementKey)
            //{
            //    //if (rightControllerManager.itemList.)
            //    foreach(RightControllerManager.CollectableItem item in rightControllerManager.itemList)
            //    {
            //        if (item.name == "Basement Key")
            //        {
            //            if (item.hasItemInInventory)
            //            {
            //                objToEnableWithBasementKey.SetActive(true);
            //            }
            //        }
            //    }
            //}

            //if (ifHasGun)
            //    foreach (RightControllerManager.CollectableItem item in rightControllerManager.itemList)
            //    {
            //        if (item.name == "Pistol")
            //        {
            //            if (item.hasItemInInventory)
            //            {
            //                objToEnableWithBasementKey.SetActive(true);
            //            }
            //        }
            //    }

            

        }
    }

    private void EnableObjects()
    {
        foreach (var obj in objsToEnable)
        {
            obj.SetActive(true);
            //print("enabled object");
        }

        if (disableColliderOnTrigger)
            this.GetComponent<BoxCollider>().enabled = false;
    }
}
