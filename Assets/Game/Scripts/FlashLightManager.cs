using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour {

    //public SteamVR_Controller.Device device;
    public SteamVR_TrackedObject trackedObj;
    //public Hand hand;

    public Light[] lights;
    public float[] lightAngles;
    public float[] lightRanges;
    //public Light spotlight30;
    //public Light spolight40;
    //public Light spotlight55;

    public float followSpeed;
    private Rigidbody rb;

    public Transform stowPos;

    public bool isStowed;
    //public Light light;
    private Ray ray;

	// Use this for initialization
	void Awake () {
        //trackedObj = hand.handTrackedLeft;
        //device = hand.handDeviceLeft;
        rb = GetComponent<Rigidbody>();
        lightAngles = new float[lights.Length];
        lightRanges = new float[lights.Length];

    }

    private void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lightAngles[i] = lights[i].spotAngle;
            lightRanges[i] = lights[i].range;
        }
        
    }

    // Update is called once per frame
    void Update () {

        /*
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.0f))
        //if (Physics.SphereCast(ray, .25f, out hit))
        {
            //print(hit.transform.gameObject.tag);

            if (hit.transform.gameObject.tag == "Collectable")
            {
                //print("I spy a collectable");
                var collectable = hit.transform.gameObject.GetComponent<Collectible>();
                collectable.CollectableSighted();
                for(int i = 0; i < lights.Length; i++)
                {
                    lights[i].spotAngle = Mathf.Lerp(lights[i].spotAngle, lightAngles[i] - (20 * collectable.foundPercentSmooth), Time.deltaTime * 3f);
                }

                /*if (collectable.isPickedUp)
                {
                    isStowed = true;
                } else
                {
                    isStowed = false;
                }*/
          //  }
        //}

        /*if (isStowed)
        {
            transform.position = Vector3.Lerp(transform.position, stowPos.position, Time.deltaTime * 3f);
            transform.rotation = Quaternion.Euler(Vector3.zero);

            foreach(Light light in lights)
            {
                light.range = Mathf.Lerp(light.range, 3, Time.deltaTime * 3f);
                light.spotAngle = Mathf.Lerp(light.spotAngle, 150, Time.deltaTime * 3f);
            }
        }
        else
        {

        }*/



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
            */


        //transform.rotation = Quaternion.Slerp(transform.rotation, trackedObj.transform.rotation, followSpeed / 2 * Time.deltaTime);


    }

    private void FixedUpdate()
    {
        if (trackedObj.gameObject.activeInHierarchy)
        {
            //if (!isStowed)
            {
                Vector3 dir = (trackedObj.transform.position - transform.position);
                rb.velocity = (dir) * followSpeed * Time.deltaTime;

                rb.MoveRotation(trackedObj.transform.rotation);
            }
           


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
