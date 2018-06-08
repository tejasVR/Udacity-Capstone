using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjCounter : MonoBehaviour {

    public float counterToDisableObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        counterToDisableObj -= Time.deltaTime;
        if (counterToDisableObj <= 0)
        {
            this.gameObject.SetActive(false);
        }
		
	}
}
