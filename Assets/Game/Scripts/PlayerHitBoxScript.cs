using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxScript : MonoBehaviour {

    public float _timeUntilNextHit;
    public float _timeUntilNextHitCounter;

	// Use this for initialization
	void Start () {
        _timeUntilNextHitCounter = _timeUntilNextHit;
		
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
        if (_timeUntilNextHitCounter == _timeUntilNextHit)
        {
            print(other.gameObject.name);
            _timeUntilNextHitCounter = 0;

            PostProcessControl.PlayerDamageEffect();
        }

        

    }
}
