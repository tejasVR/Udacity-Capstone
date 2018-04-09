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

    public Renderer[] rends;

    public Light pointLight;

    public Transform attachPoint;
    //public GameObject playerEye;

    private Vector3 lastPos; //Last position before collectable is picked up
    private Quaternion lastRot; //Last rotation before collectable is picked up

    public bool isNote;
    public bool isReel;

	// Use this for initialization
	void Start () {
        foundMeter = 0;
        pointLight.enabled = false;
        //rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        foundPercentSmooth = Mathf.Lerp(foundPercentSmooth, ((foundMeter / foundMeterMax)), Time.deltaTime * 10f);       

        foundColor = Color.Lerp(Color.black, Color.white, foundPercentSmooth);

        for(int i = 0; i < rends.Length; i++)
        {
            rends[i].material.SetColor("_EmissionColor", new Vector4(foundColor.r, foundColor.g, foundColor.b, 0));
        }

        if (isPickedUp & !isSentBack)
        {
            transform.position = Vector3.Lerp(transform.position, attachPoint.transform.position, Time.deltaTime * 3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, attachPoint.transform.rotation, Time.deltaTime * 3f);

            //rightControllerManager.currentInHand = this.gameObject;

            rightControllerManager.HideFlashlight();

            pointLight.enabled = true;

            foundMeter = 0;

            if(device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (isNote)
                {
                    isPickedUp = false;
                    isSentBack = true;
                }

                if (isReel)
                {
                    isPickedUp = false;
                    isSentBack = true;
                    rightControllerManager.ShowFlashlight();
                }
               

            }
            //transform.LookAt(playerEye.transform.position);
        }

        if (isSentBack && !isPickedUp && isNote)
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
