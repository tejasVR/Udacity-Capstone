using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLowPassFilter : MonoBehaviour {

    private AudioLowPassFilter _lowPass;
    private float _lowPassOriginal;
    private static float _lowPassMaster;

	// Use this for initialization
	void Start () {

        _lowPass = GetComponent<AudioLowPassFilter>();
        _lowPassOriginal = _lowPass.cutoffFrequency;

	}
	
	// Update is called once per frame
	void Update () {

        if (Mathf.Abs(_lowPassMaster - _lowPass.cutoffFrequency) < 100)
        {
            _lowPassMaster = Mathf.Lerp(_lowPassMaster, _lowPassOriginal, Time.deltaTime);
            _lowPass.cutoffFrequency = _lowPassMaster;
        }
       
	}

    public static void LowPassFilterAmount(float lowPassAmount)
    {
        _lowPassMaster = lowPassAmount;
    }


}
