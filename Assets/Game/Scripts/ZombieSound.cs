using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour {

    // 0 = left foot, 1 = right foot, 2 = head
    public AudioSource[] _audioBody;
   // public GameObject[] _bodyPart;

    public AudioClip[] _woodStep;


	// Use this for initialization
	void Start () {
        //foreach (var part in _bodyPart)
        //{
        //    part.
        //}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeStepOnWood(int bodyPart)
    {
        PlaySound.PlayAudioFromSelection(_audioBody[bodyPart], _woodStep, true, -.1f, .1f);
    }
}
