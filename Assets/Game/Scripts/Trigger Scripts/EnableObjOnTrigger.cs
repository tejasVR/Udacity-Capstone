﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjOnTrigger : MonoBehaviour {

    public RightControllerManager rightControllerManager;

    public GameObject[] objsToEnable;

    public bool _disableObjOnTrigger;

    public GameObject[] objsToDisable;
    //public GameObject objToEnableWithBasementKey;
    //public GameObject objToEnableWithGun;



    public bool disableColliderOnTrigger;

    public enum HasItem
    {
        none,
        atticKey,
        basementKey,
        pistol,
        frontDoorKey

    }

    public HasItem hasItem;

    private void Awake()
    {
        foreach (var obj in objsToEnable)
        {
            obj.SetActive(false);
            //print("Disabling:" + obj.gameObject.name);
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

            if (hasItem == HasItem.atticKey)
            {
                foreach (RightControllerManager.InventorySlot item in rightControllerManager._inventorySlots)
                {
                    if (item.name == "Attic Key")
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
        if (objsToEnable.Length >= 1)
        {
            foreach (var obj in objsToEnable)
            {
                obj.SetActive(true);
                //print("enabled object");
            }
        }

        if (_disableObjOnTrigger)
        {
            foreach (var obj in objsToDisable)
            {
                obj.SetActive(false);
                //print("enabled object");
            }
        }

        if (disableColliderOnTrigger)
            this.GetComponent<BoxCollider>().enabled = false;
    }
}
