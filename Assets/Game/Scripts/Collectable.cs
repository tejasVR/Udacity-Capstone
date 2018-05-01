using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    //public RightControllerManager rightControllerManager;

    //public GameObject flashlightObj;

    //public float foundMeter;
    //public float foundMeterMax;

    //public float foundPercentSmooth;

    //private Color foundColor;
    //public bool isSeen; //when the flashlight sees the object
    //public bool isPickedUp; //when the player can interact with the object
    //public bool firstCollected;
    //public bool isSentBack; //when the player wants to put the object back
    //public bool isSentFromHand;

    //public Renderer[] rends;

    //public Light pointLight;

    //public Transform attachPoint;
    //public GameObject playerEye;

    //private Vector3 lastPos; //Last position before collectable is picked up
    //private Quaternion lastRot; //Last rotation before collectable is picked up

    // The material that the collectable takes when a controller hovers over the object
    public Material normalMat;
    public Material onHoverMat;

    public bool isInHand;

    Renderer rend;

    public string itemName;

    public Vector3 inHandPos;
    public Quaternion inHandRot;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();
        normalMat = rend.material;

        //foundMeter = 0;
        //pointLight.enabled = false;
        //rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        #region OLD_CODE
        /*SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        foundPercentSmooth = Mathf.Lerp(foundPercentSmooth, ((foundMeter / foundMeterMax)), Time.deltaTime * 10f);       

        foundColor = Color.Lerp(Color.black, Color.white, foundPercentSmooth);

        for(int i = 0; i < rends.Length; i++)
        {
            rends[i].material.SetColor("_EmissionColor", new Vector4(foundColor.r, foundColor.g, foundColor.b, 0));
        }

       transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 3f);

        if (isPickedUp)
        {

            HoldInHand();

            //rightControllerManager.currentInHand = this.gameObject;

            //rightControllerManager.HideFlashlight();

            pointLight.enabled = true;

            foundMeter = 0;

            if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !firstCollected)
            {
                if (isSentFromHand)
                {
                    rightControllerManager.CollectItem(itemName, this.gameObject);
                    isPickedUp = false;
                }
                //if (!isSentFromHand)
                // {
                //}

                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                //this.gameObject.SetActive(false);
                /*if (isNote)
                {
                    isPickedUp = false;
                    isSentBack = true;
                }*/

        //if (isReel)
        /*{

            //isSentBack = true;
            //rightControllerManager.ShowFlashlight();
        }


    }

    //if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && isSentFromHand)
    {

    }
    //transform.LookAt(playerEye.transform.position);
}*/

        /*if (!isPickedUp && isNote)
        {
            transform.position = Vector3.Lerp(transform.position, lastPos, Time.deltaTime * 3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, lastRot, Time.deltaTime * 10f);

            if (Vector3.Distance(transform.position, lastPos) < .01)
            {
                isSentBack = false;
            }
        }*/
        #endregion

        if (isInHand)
        {
            //Quaternion rot = trackedObj.transform.rotation * inHandRot;
            transform.position = Vector3.Lerp(transform.position, trackedObj.transform.position + inHandPos, Time.deltaTime * 12f);
            transform.rotation = Quaternion.Slerp(transform.rotation, trackedObj.transform.rotation * inHandRot, Time.deltaTime * 12f);
            
        }




    }

    private void OnCollisionStay(Collision collision)
    {
        // If we are triggering against the players' controller
        if (collision.gameObject.tag == "Controller" && !isInHand)
        {
            rend.material = onHoverMat;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Controller" && !isInHand)
        {
            rend.material = normalMat;
        }      
    }

    #region OLD_FUNCTIONS
    /*
    public void CollectableSighted()
    {
        if (!isPickedUp & !firstCollected)
        {
            foundMeter += Time.deltaTime * 25f;
            if (foundMeter >= foundMeterMax)
            {
                // Get curent position/rotation values
                //lastPos = transform.position;
                //lastRot = transform.rotation;


                isPickedUp = true;
            }
        }
    }


    public void HoldInHand()
    {
        transform.position = Vector3.Lerp(transform.position, attachPoint.transform.position, Time.deltaTime * 3f);
        transform.rotation = Quaternion.Lerp(transform.rotation, attachPoint.transform.rotation, Time.deltaTime * 3f);
    }

    */
    #endregion
}
