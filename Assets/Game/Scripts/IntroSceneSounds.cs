using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneSounds : MonoBehaviour {

    private AudioSource _audioSource;

    public float _audioStartPos;
    public float _audioDelay;

    public bool _shouldMove;

    public Vector3 endPos;
    public float _moveSpeed;
    public float _moveDelay;
    private float _moveDelayCounter;


    public bool _shouldFadeIn;
    public float _fadeInDelay;
    //private float _fadeInTimeCounter;
    public float _fadeInSpeed;

    public bool _shouldFadeOut;
    public float _fadeOutDelay;
    //private float _fadeOutTimeCounter;
    public float _fadeOutSpeed;

   

    private float _volumeOriginal;

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
        _volumeOriginal = _audioSource.volume;

        if (_shouldFadeIn)
        {
            _audioSource.volume = 0;
        }

        //_moveDelayCounter = _moveDelay;
        //_fadeOutTimeCounter = _fadeOutTime;

        StartCoroutine(PlayAudio());

	}
	
	// Update is called once per frame
	void Update () {
		if (_shouldMove)
        {
            MovePos();
        }

        if (_shouldFadeIn)
        {
            if (_shouldFadeOut)
            {
                if (_fadeOutDelay >= .5f)
                {
                    FadeIn();
                }
            }
            else
                FadeIn();
            
        }

        if (_shouldFadeOut)
        {
            //if (_shouldFadeIn)
            //{
            //    if (_fadeInDelay < 0)
            //    {
            //        FadeOut();
            //    }
            //}
            //else
                FadeOut();
            //print("calling fade out");
        }
    }

    private IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(_audioDelay);
        _audioSource.time = _audioStartPos;
        _audioSource.Play();
    }

    //private IEnumerator MovePos()
    //{
    //    yield return new WaitForSeconds(1f);
    //}


    private void FadeIn()
    {
        if (_fadeInDelay > 0)
            _fadeInDelay -= Time.deltaTime;
        else
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, _volumeOriginal, Time.deltaTime * _fadeInSpeed);
    }

    private void FadeOut()
    {
        if (_fadeOutDelay > 0)
            _fadeOutDelay -= Time.deltaTime;
        else
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0, Time.deltaTime * _fadeOutSpeed);
    }

    private void MovePos()
    {
        if (_moveDelay > 0)
            _moveDelay -= Time.deltaTime;
        else
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * _moveSpeed);
    }

}
