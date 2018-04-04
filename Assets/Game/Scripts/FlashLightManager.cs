using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour {

    //public SteamVR_Controller.Device device;
    public SteamVR_TrackedObject trackedObj;
    //public Hand hand;

    public Light[] lights;
    public float[] lightAngle;
    //public Light spotlight30;
    //public Light spolight40;
    //public Light spotlight55;

    public float followSpeed;
    private Rigidbody rb;
    //public Light light;
    private Ray ray;

	// Use this for initialization
	void Awake () {
        //trackedObj = hand.handTrackedLeft;
        //device = hand.handDeviceLeft;
        rb = GetComponent<Rigidbody>();
        lightAngle = new float[lights.Length];
    }

    private void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lightAngle[i] = lights[i].spotAngle;
        }
        
    }

    // Update is called once per frame
    void Update () {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        //if (Physics.SphereCast(ray, .25f, out hit))
        {
            //print(hit.transform.gameObject.tag);

            if (hit.transform.gameObject.tag == "Collectable")
            {
                print("I spy a collectable");
                var collectable = hit.transform.gameObject.GetComponent<Collectible>();
                collectable.CollectableSighted();
                for(int i = 0; i < lights.Length; i++)
                {
                    lights[i].spotAngle = Mathf.Lerp(lights[i].spotAngle, lightAngle[i] - (20 * collectable.foundPercentSmooth), Time.deltaTime * 3f);
                }
            }
        }


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
            Vector3 dir = (trackedObj.transform.position - transform.position);
            rb.velocity = (dir) * followSpeed * Time.deltaTime;
            
            rb.MoveRotation(trackedObj.transform.rotation);


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
