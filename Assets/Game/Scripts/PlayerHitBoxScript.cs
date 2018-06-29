using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxScript : MonoBehaviour {

    public float health;

    public float _timeUntilNextHit;
    public float _timeUntilNextHitCounter;

    private AudioSource _audioSource;
    public AudioClip[] _hitClips;

    private float _heartBeatPitch = 1;

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
        if (other.gameObject.layer == 21)
        {
            if (health > 0)
            {
                if (_timeUntilNextHitCounter == _timeUntilNextHit)
                {
                    PlaySound.PlayAudioFromSelection(_audioSource, _hitClips, true, -.15f, .15f);
                    //HeartBeatControl.HeartBeatPitchAmount(1.2f);
                    //print(other.gameObject.name);
                    _timeUntilNextHitCounter = 0;
                    //GlobalLowPassFilter.LowPassFilterAmount(500);
                    PostProcessControl.PlayerDamagePostEffect(_playerDamageVignetteAmnt, _playerDamageContrastAmnt);
                    _heartBeatPitch += .1f;
                    HeartBeatControl.HeartBeatPitchAmount(_heartBeatPitch);
                    StartCoroutine(HitHaptics(.25f));
                    //HapticFeedback.HapticAmount(1500, true, true);
                    health--;
                }

                if (health <= 1)
                {
                    //
                }
            }
            else
            {
                StartCoroutine(Death());
            }
        }
    }

    //private void HitHaptics()
    //{
    //    for (int i = 1000; i > 0; i++)
    //    {
    //        if(i % 10 == 0)
    //        {
    //            HapticFeedback.HapticAmount((int)(1.5f * i), true, true);
    //        }
    //    }
    //}


    IEnumerator HitHaptics(float timeBetween)
    {
        for (int i = 5; i > 0; i--)
        {
            yield return new WaitForSeconds(timeBetween);
            HapticFeedback.HapticAmount((int)((200 * i) + 1000), true, false);
        }
    }

    IEnumerator Death()
    {
        PostProcessControl.PostExposureFade(-10, .01f);
        yield return new WaitForSeconds(2f);
        GameManager.NextScene();

    }

}
