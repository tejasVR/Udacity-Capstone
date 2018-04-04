using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControllerManager : MonoBehaviour {

    public GameObject flashLight;
    public GameObject collectable;
    public GameObject currentInHand; //current GameObject in players hand

	// Use this for initialization
	void Start () {
        if (currentInHand.gameObject == null)
        {
            flashLight.gameObject.SetActive(true);
            currentInHand = flashLight;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (currentInHand.tag != "Flashlight" && currentInHand != null)
        {
            //flashLight.gameObject.SetActive(false);
        } else
        {
            //flashLight.gameObject.SetActive(true);
            currentInHand = flashLight;
        }

        if (currentInHand == null)
        {
            //flashLight.gameObject.SetActive(true);
            currentInHand = flashLight;
        }
		
	}
}
