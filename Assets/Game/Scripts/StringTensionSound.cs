using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringTensionSound : MonoBehaviour {

    private AudioSource _audioSource;
    public Transform _startPoint;
    public Transform _endPoint;

    private float _distanceTotal;
    private float _distanceCurrent;

    private float _volumeTarget;

    private void Awake()
    {
        _volumeTarget = _audioSource.volume;
    }

    // Use this for initialization
    void Start () {

        _audioSource = GetComponent<AudioSource>();
        _startPoint.position = transform.position;
	}



    private void OnEnable()
    {
        _audioSource.volume = 0;
        _distanceTotal = Vector3.Distance(_startPoint.position, _endPoint.position);
    }

    // Update is called once per frame
    void Update () {
        _distanceCurrent = Vector3.Distance(PlayerScript._playerEye.transform.position, _endPoint.position);

        _audioSource.volume = (_distanceTotal - _distanceCurrent) / _distanceTotal;
		
	}
}
