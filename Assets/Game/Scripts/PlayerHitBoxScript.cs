using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxScript : MonoBehaviour {

    public float health;

    public float _timeUntilNextHit;
    public float _timeUntilNextHitCounter;

    private AudioSource _audioSource;
    public AudioClip[] _hitClips;

    [Header("Player Damage Effect Properties")]
    public float _playerDamageVignetteAmnt;
    public float _playerDamageContrastAmnt;

	// Use this for initialization
	void Start () {
        _timeUntilNextHitCounter = _timeUntilNextHit;
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (_timeUntilNextHitCounter < _timeUntilNextHit)
            _timeUntilNextHitCounter += Time.deltaTime;
        else if (_timeUntilNextHitCounter > _timeUntilNextHit)
            _timeUntilNextHitCounter = _timeUntilNextHit;

	}

    private void OnTriggerEnter(Collider other)
    {
        if(health > 0)
        {
            if (_timeUntilNextHitCounter == _timeUntilNextHit)
            {
                PlaySound.PlayAudioFromSelection(_audioSource, _hitClips, true, -.15f, .15f);
                HeartBeatControl.HeartBeatPitchAmount(1.2f);
                //print(other.gameObject.name);
                _timeUntilNextHitCounter = 0;
                GlobalLowPassFilter.LowPassFilterAmount(500);
                PostProcessControl.PlayerDamagePostEffect(_playerDamageVignetteAmnt, _playerDamageContrastAmnt);
                health--;
            }

            if (health <= 1)
            {
                PostProcessControl.PostExposureFade(-10, .2f);
            }
        }
        else
        {
            GameManager.NextScene();
        }
    }
}
