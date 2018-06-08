using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    private Light light;

    public float _minIntensity;
    public float _maxIntensity;

    public float _changeFrequencyMin;
    public float _changeFrequencyMax;

    //[Range(0.0f,1.0f)]
    //public float _stopChance; // in %
    //public float _stopDuration;

    private float _frequencyTimer;
    //public float _stopDurationTimer;

    //public bool isStopped;

    //public float lightFadeSpeed;

	// Use this for initialization
	void Start () {

        light = GetComponent<Light>();

        _frequencyTimer = Random.Range(_changeFrequencyMin,_changeFrequencyMax);
        //_stopDurationTimer = _stopDuration;
	}
	
	// Update is called once per frame
	void Update () {

        if (_frequencyTimer > 0)
        {
            _frequencyTimer -= Time.deltaTime;

        } else 
        {
            //if (!isStopped)
            {
                var newIntensity = Random.Range(_minIntensity, _maxIntensity);
                light.intensity = Mathf.Lerp(light.intensity, newIntensity, Time.deltaTime * 5f);
            }
            

            /*if (Random.Range(0f,1f) > _stopChance)
            {
                isStopped = true;
                _stopDurationTimer -= Time.deltaTime;
                light.intensity -= Time.deltaTime * lightFadeSpeed;

                if (_stopDurationTimer <= 0)
                {
                    
                    _stopDurationTimer = _stopDuration;
                    _frequencyTimer = Random.Range(_changeFrequencyMin, _changeFrequencyMax);
                    isStopped = false;
                }
            }*/
        }
		
	}
}
