using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatControl : MonoBehaviour {

    private AudioSource _audioSource;
    private float _pitchOriginal;
    private static float _pitchMaster;

	// Use this for initialization
	void Start () {

        _audioSource = GetComponent<AudioSource>();
        _pitchOriginal = _audioSource.pitch;

	}
	
	// Update is called once per frame
	void Update () {

        if (Mathf.Abs(_pitchMaster - _audioSource.pitch) < .01f)
        {
            _pitchMaster = Mathf.Lerp(_pitchMaster, _pitchOriginal, Time.deltaTime / 10);
            _audioSource.pitch = _pitchMaster;
        }
	}

    public static void HeartBeatPitchAmount(float pitchToSet)
    {
        _pitchMaster = pitchToSet;
    }
}
