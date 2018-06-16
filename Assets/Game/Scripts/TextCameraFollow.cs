using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCameraFollow : MonoBehaviour {

    public bool _shouldDelay;
    public bool _shouldMoveWithCamera;

    public float _visibilityDelay;
    public float _visibilityDuration;
    public int _distanceFromCamera;
    public float _moveSpeed;
    private float _disableObjCounter;

    public GameObject _player;
    public GameObject _textObj;

    

	// Use this for initialization
	void Start () {
        if (_shouldDelay)
            _textObj.SetActive(false);

        _disableObjCounter = _visibilityDelay + _visibilityDuration;
        //if (_shouldMoveWithCamera)
        //    GetPos();
	}

    private void OnEnable()
    {
        GetPos();
    }

    // Update is called once per frame
    void Update () {

        _disableObjCounter -= Time.deltaTime;
        if (_disableObjCounter <= 0)
        {
            this.gameObject.SetActive(false);
        }

        if (_shouldDelay)
        {
            if (_visibilityDelay > 0)
            {
                _visibilityDelay -= Time.deltaTime;
                if (_visibilityDelay <= 0)
                {
                    _textObj.SetActive(true);
                }
            }
        }

        if (_textObj.activeInHierarchy)
        {
            _visibilityDuration -= Time.deltaTime;
            if (_visibilityDuration <= 0)
            {
                _textObj.SetActive(false);
            }
        }

        if (_shouldMoveWithCamera && _textObj.activeInHierarchy)
        {
            GetPos();
        }
        
		
	}

    private void GetPos()
    {
        Ray ray = new Ray(_player.transform.position, _player.transform.forward);
        var point = ray.GetPoint(_distanceFromCamera);

        transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime * _moveSpeed);
        transform.LookAt(_player.transform.position);
    }

   
}
