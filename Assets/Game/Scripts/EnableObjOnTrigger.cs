using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjOnTrigger : MonoBehaviour {

    public RightControllerManager rightControllerManager;

    public GameObject objToEnable;
    public GameObject objToEnableWithBasementKey;
    public GameObject objToEnableWithGun;



    public bool disableColliderOnTrigger;

    public bool ifHasItem;
    public bool ifHasBasementKey;
    public bool ifHasGun;


    private void Start()
    {
        objToEnable.SetActive(false);
        objToEnableWithBasementKey.SetActive(false);
        objToEnableWithGun.SetActive(false);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!ifHasItem)
                objToEnable.SetActive(true);

            if (ifHasBasementKey)
            {
                //if (rightControllerManager.itemList.)
                foreach(RightControllerManager.CollectableItem item in rightControllerManager.itemList)
                {
                    if (item.name == "Basement Key")
                    {
                        if (item.hasItemInInventory)
                        {
                            objToEnableWithBasementKey.SetActive(true);
                        }
                    }
                }
            }

            if (ifHasGun)
                foreach (RightControllerManager.CollectableItem item in rightControllerManager.itemList)
                {
                    if (item.name == "Pistol")
                    {
                        if (item.hasItemInInventory)
                        {
                            objToEnableWithBasementKey.SetActive(true);
                        }
                    }
                }

            if (disableColliderOnTrigger)
                this.GetComponent<BoxCollider>().enabled = false;

        }
    }
}
