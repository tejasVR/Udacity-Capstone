using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLowPassFilter : MonoBehaviour {

    private AudioLowPassFilter lowPass;
    private float lowPassOriginal;
    private static float lowPassMaster;

	// Use this for initialization
	void Start () {

        lowPass = GetComponent<AudioLowPassFilter>();
        lowPassOriginal = lowPass.cutoffFrequency;

	}
	
	// Update is called once per frame
	void Update () {

        if (Mathf.Abs(lowPassMaster - lowPass.cutoffFrequency) < 100)
        {
            lowPassMaster = Mathf.Lerp(lowPassMaster, lowPassOriginal, Time.deltaTime);
            lowPass.cutoffFrequency = lowPassMaster;
        }
       
	}

    public static void LowPassFilterAmount(float lowPassAmount)
    {
        lowPassMaster = lowPassAmount;
    }


}
