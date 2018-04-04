using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    //public GameObject flashlightObj;

    public float foundMeter;
    public float foundMeterMax;

    public float foundPercentSmooth;

    private Color foundColor;
    public bool isSeen; //when the flashlight sees the object
    public bool isFound; //when the player can interact with the object
    private Renderer rend;


	// Use this for initialization
	void Start () {
        foundMeter = 0;
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        foundPercentSmooth = Mathf.Lerp(foundPercentSmooth, ((foundMeter / foundMeterMax)), Time.deltaTime * 10f);

       
       
        {
            isSeen = false;
            foundMeter -= Time.deltaTime * 10f;
            if (foundMeter <= 0)
            {
                foundMeter = 0;
            }
        }

        foundColor = Color.Lerp(Color.black, Color.white, foundPercentSmooth);

        rend.material.SetColor("_EmissionColor", new Vector4(foundColor.r, foundColor.g, foundColor.b, 0));
        //rend.material.SetFloat("_EmissionScaleUI", (foundMeter/foundMeterMax));


    }

    private void FixedUpdate()
    {

    }

    public void CollectableSighted()
    {
        if (!isFound)
        {
            foundMeter += Time.deltaTime * 10f;
            if (foundMeter >= foundMeterMax)
            {
                isFound = true;
            }
        }
    }
}
