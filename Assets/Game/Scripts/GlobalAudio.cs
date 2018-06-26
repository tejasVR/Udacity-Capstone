using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : MonoBehaviour {

    //private AudioListener _audioListener;
    private AudioLowPassFilter _lowPass;
    private float _lowPassOriginal;
    private static float _lowPassMaster;

    private static float _volumeMaster;

	// Use this for initialization
	void Start () {

        _lowPass = GetComponent<AudioLowPassFilter>();
        _lowPassOriginal = _lowPass.cutoffFrequency;
        //_audioListener = GetComponent<AudioListener>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Mathf.Abs(_lowPassMaster - _lowPass.cutoffFrequency) < 100)
        {
            _lowPassMaster = Mathf.Lerp(_lowPassMaster, _lowPassOriginal, Time.deltaTime);
            _lowPass.cutoffFrequency = _lowPassMaster;
        }

        if (Mathf.Abs(_volumeMaster - AudioListener.volume) > .01f)
        {
            AudioListener.volume = Mathf.Lerp(AudioListener.volume, _volumeMaster, Time.deltaTime * .2f);
            //_lowPassMaster = Mathf.Lerp(_lowPassMaster, _lowPassOriginal, Time.deltaTime);
            //_lowPass.cutoffFrequency = _lowPassMaster;
            //print(AudioListener.volume);
        }



    }

    public static void LowPassFilterAmount(float lowPassAmount)
    {
        _lowPassMaster = lowPassAmount;
    }

    public static void GlobalAudioFadeOut()
    {
        
        //AudioListener.volume = Mathf.Lerp(AudioListener.volume, 0, Time.deltaTime * .2f);
        _volumeMaster = 0;
    }

    public static void GlobalAudioFadeIn()
    {
        AudioListener.volume = 0;
        //AudioListener.volume = Mathf.Lerp(AudioListener.volume, 1f, Time.deltaTime * .2f);
        _volumeMaster = 1;
    }


}
