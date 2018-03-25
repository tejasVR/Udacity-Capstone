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

        /*if (trackedObj.gameObject.activeInHierarchy)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

            //Vector3 direction = 
            

            //rb.MovePosition(trackedObj.transform.position);

            rb.position = Vector3.Lerp(transform.position, trackedObj.transform.position, followSpeed * Time.deltaTime);
            rb.rotation = Quaternion.Slerp(transform.rotation, trackedObj.transform.rotation, followSpeed * Time.deltaTime);

            //rb.AddForce()

            //transform.position = Vector3.Lerp(transform.position, trackedObj.transform.position, followSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, trackedObj.transform.rotation, followSpeed * Time.deltaTime);
        }*/

        
    }

    private void FixedUpdate()
    {
        if (trackedObj.gameObject.activeInHierarchy)
        {
            Vector3 dir = (trackedObj.transform.position - transform.position);
            rb.velocity = (dir) * followSpeed * Time.deltaTime;
            rb.rotation = trackedObj.transform.rotation;


            //rb.MovePosition(transform.position + dir * followSpeed * Time.deltaTime);

            //
            

            //rb.MovePosition(trackedObj.transform.position * Time.deltaTime);
            //rb.MoveRotation(trackedObj.transform.rotation);
            //rb.MovePosition(Vector3.Lerp(transform.position, trackedObj.transform.position, followSpeed * Time.deltaTime));
            //rb.MoveRotation(Quaternion.Slerp(transform.rotation, trackedObj.transform.rotation, followSpeed * Time.deltaTime));

            //rb.position = trackedObj.transform.position;
            //rb.rotation = trackedObj.transform.rotation;

        }
    }
}
