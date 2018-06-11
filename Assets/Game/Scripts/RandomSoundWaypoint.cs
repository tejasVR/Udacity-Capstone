using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundWaypoint : MonoBehaviour {

    public AudioClip[] _clips;
    private AudioSource _audioSource;
    public Transform[] _waypoints;

    private int _randomIntClip;
    private int _randomIntPos;

    private float _clipLength;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        Randomize();
	}
	
	// Update is called once per frame
	void Update () {

        if (_clipLength <= 0)
            Randomize();
        else
            _clipLength -= Time.deltaTime;
    }

    private void Randomize()
    {
        _randomIntClip = Random.Range(0, _clips.Length - 1);
        _randomIntPos = Random.Range(0, _waypoints.Length - 1);

        var randomPos = _waypoints[_randomIntPos].transform.position;
        var randomClip = _clips[_randomIntClip];
        var randomDelay = Random.Range(0.5f, 1.5f);

        transform.position = randomPos;
        _clipLength = randomClip.length + randomDelay;
        _audioSource.clip = randomClip;
        _audioSource.PlayDelayed(randomDelay);
    }
}
