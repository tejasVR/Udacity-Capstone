using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTensionSound : MonoBehaviour {

    private AudioSource _audioSource;
    private float _clipLength;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _clipLength = _audioSource.clip.length;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(GoToEnding());
	}

    IEnumerator GoToEnding()
    {
        yield return new WaitForSeconds(_clipLength);
        GameManager.NextScene();
    }
}
