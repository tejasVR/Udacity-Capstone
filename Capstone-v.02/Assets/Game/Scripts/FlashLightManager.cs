using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour {

    //public SteamVR_Controller.Device device;
    public SteamVR_TrackedObject trackedObj;
    //public Hand hand;

    public float followSpeed;
    private Rigidbody rb;

	// Use this for initialization
	void Awake () {
        //trackedObj = hand.handTrackedLeft;
        //device = hand.handDeviceLeft;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //trackedObj = hand.handTrackedLeft;
        //device = hand.handDeviceLeft;

        if (trackedObj.gameObject.activeInHierarchy)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

            //rb.MovePosition(Vector3.Lerp(transform.position, trackedObj.transform.position, followSpeed * Time.deltaTime));
            //rb.MoveRotation(Quaternion.Slerp(transform.rotation, trackedObj.transform.rotation, followSpeed * Time.deltaTime));

            rb.MovePosition(trackedObj.transform.position);

            //transform.position = Vector3.Lerp(transform.position, trackedObj.transform.position, followSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, trackedObj.transform.rotation, followSpeed * Time.deltaTime);
        }

        
    }
}
