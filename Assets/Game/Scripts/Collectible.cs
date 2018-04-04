using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public RightControllerManager rightControllerManager;

    //public GameObject flashlightObj;

    public float foundMeter;
    public float foundMeterMax;

    public float foundPercentSmooth;

    private Color foundColor;
    public bool isSeen; //when the flashlight sees the object
    public bool isPickedUp; //when the player can interact with the object
    public bool isSentBack; //when the player wants to put the object back

    public Renderer rend;

    public Transform readPoint;
    //public GameObject playerEye;

    private Vector3 lastPos; //Last position before collectable is picked up
    private Quaternion lastRot; //Last rotation before collectable is picked up

    public bool isNote;

	// Use this for initialization
	void Start () {
        foundMeter = 0;
        //rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        foundPercentSmooth = Mathf.Lerp(foundPercentSmooth, ((foundMeter / foundMeterMax)), Time.deltaTime * 10f);

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        
       
        
            //isSeen = false;
            //foundMeter -= Time.deltaTime * 10f;
            //if (foundMeter <= 0)
            //{
              //  foundMeter = 0;
            //}
        

        foundColor = Color.Lerp(Color.black, Color.white, foundPercentSmooth);

        rend.material.SetColor("_EmissionColor", new Vector4(foundColor.r, foundColor.g, foundColor.b, 0));
        //rend.material.SetFloat("_EmissionScaleUI", (foundMeter/foundMeterMax));

        if (isPickedUp & !isSentBack)
        {
            transform.position = Vector3.Lerp(transform.position, readPoint.transform.position, Time.deltaTime * 3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, readPoint.transform.rotation, Time.deltaTime * 3f);

            rightControllerManager.currentInHand = this.gameObject;

            foundMeter = 0;

            if(device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && isNote)
            {
                isPickedUp = false;
                isSentBack = true;
                rightControllerManager.currentInHand = null;

            }
            //transform.LookAt(playerEye.transform.position);
        }

        if (isSentBack && !isPickedUp)
        {
            transform.position = Vector3.Lerp(transform.position, lastPos, Time.deltaTime * 3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, lastRot, Time.deltaTime * 10f);

            if (Vector3.Distance(transform.position, lastPos) < .01)
            {
                isSentBack = false;
            }
        }

    }

    public void CollectableSighted()
    {
        if (!isPickedUp && !isSentBack)
        {
            foundMeter += Time.deltaTime * 25f;
            if (foundMeter >= foundMeterMax)
            {
                // Get curent position/rotation values
                lastPos = transform.position;
                lastRot = transform.rotation;


                isPickedUp = true;
            }
        }
    }
}
