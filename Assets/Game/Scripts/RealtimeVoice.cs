using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeVoice : MonoBehaviour {

	// Use this for initialization
	void Start () {

        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start("Built-in Microphone", true, 1, 22050);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("start playing... position is " + Microphone.GetPosition(null));
        audio.Play();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
