using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningGramophone : MonoBehaviour {

    public MenuScript _menuScript;
    private IntroSceneSounds _gramophone;
    private AudioSource _audioSource;

    public float _fadeOutDelay;
    public float _fadeOutSpeed;

    private bool _fadeOut;

    private void Start()
    {
        _gramophone = GetComponent<IntroSceneSounds>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_menuScript._isProgressBarFilled)
        {
            //_gramophone.enabled = true;
            FadeOut();
        }
    }

    private void FadeOut()
    {
        if (_fadeOutDelay > 0)
            _fadeOutDelay -= Time.deltaTime;
        else
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0, Time.deltaTime * _fadeOutSpeed);
    }

}
