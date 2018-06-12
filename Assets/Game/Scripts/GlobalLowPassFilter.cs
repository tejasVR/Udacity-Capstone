using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLowPassFilter : MonoBehaviour {

    private AudioLowPassFilter lowPass;
    private float lowPassOriginal;
    private float lowPassMaster;

	// Use this for initialization
	void Start () {

        lowPass = GetComponent<AudioLowPassFilter>();
        lowPassOriginal = lowPass.cutoffFrequency;

	}
	
	// Update is called once per frame
	void Update () {

        lowPassMaster = Mathf.Lerp(lowPassMaster, lowPassOriginal, Time.deltaTime / 2);
        lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, lowPassMaster, Time.deltaTime / 2f);
	}

    public void GunShotLowPass(float lowPassAmount)
    {
        lowPassMaster = lowPassAmount;
    }


}
