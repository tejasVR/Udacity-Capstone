using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFollowObject : MonoBehaviour {

    public bool _followIndefinitely;
    [Space(10)]

    //public bool _shouldDelay;
    public float _visibilityDelay;

    [Space(10)]

    public float _visibilityDuration;

    [Space(10)]

    public bool _shouldMoveWithObj;
    public float _moveSpeed;

    public bool _shouldFadeIn;
    public float _fadeInSpeed;

    [Space(10)]

    public bool _shouldFadeOut;
    public float _fadeOutSpeed;

    [Space(10)]

    public bool _shouldDisable;
    private float _disableObjCounter;

    [Space(10)]


    public int _distanceFromObject;

    public bool _faceCamera;

    public GameObject _followObject;
    public TextMeshPro _textObj;

    private bool _fadeIn;
    private bool _fadeOut;

	// Use this for initialization
	void Start () {

        if (!_followIndefinitely)
        {
            if (_shouldDisable)
                StartCoroutine(Disable());
        }

        StartCoroutine(Enable());


        //if (_shouldDelay)
        //    StartCoroutine(Delay());
        //else
        //{
        //    StartCoroutine(Enable());
        //}


        //if (_disableObjCounter)
        //{
        //    _disableObjCounter = _visibilityDelay + _visibilityDuration;
        //    StartCoroutine(Disable());
        //}
        //if (_shouldMoveWithCamera)
        //    GetPos();
    }

    private void OnEnable()
    {
        //GetPos();
    }

    // Update is called once per frame
    void Update () {

        if (_fadeIn)
        {
            ObjectFade.TextFadeIn(_textObj, 1f, _fadeInSpeed);
        }

        //_disableObjCounter -= Time.deltaTime;
        //if (_disableObjCounter <= 0)
        //{
        //    this.gameObject.SetActive(false);
        //}

        //if (_shouldDelay)
        //{
        //    if (_visibilityDelay > 0)
        //    {
        //        _visibilityDelay -= Time.deltaTime;
        //        if (_visibilityDelay <= 0)
        //        {
        //            _textObj.gameObject.SetActive(true);
        //        }
        //    }
        //}

        //if (_textObj.gameObject.activeInHierarchy)
        //{
        //    if (_visibilityDuration > 0)
        //    {
        //        _visibilityDuration -= Time.deltaTime;
        //        if (_visibilityDuration <= 0)
        //        {
                    
        //            //_textObj.SetActive(false);
        //            //this.gameObject.SetActive(false);
        //        }
        //    }
            
        //}

        if (_fadeOut)
        {

            ObjectFade.TextFadeOut(_textObj, _fadeOutSpeed, true);
            //print("trying to fade");
        }

        if (_shouldMoveWithObj)// && _textObj.gameObject.activeInHierarchy)
        {
            GetPos();
        }
        
		
	}

    private void GetPos()
    {
        Ray ray = new Ray(_followObject.transform.position, _followObject.transform.forward);
        var point = ray.GetPoint(_distanceFromObject);

        transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime * _moveSpeed);
        
        if (_faceCamera)
            transform.LookAt(PlayerScript._playerEye.transform);
        else
            transform.LookAt(_followObject.transform);
    }

    IEnumerator Disable()
    {
        _disableObjCounter = _visibilityDelay + _visibilityDuration + (1/_fadeOutSpeed);
        yield return new WaitForSeconds(_disableObjCounter);
        this.gameObject.SetActive(false);
    }

    IEnumerator Delay()
    {
        print("delaying");
        _textObj.gameObject.SetActive(false);
        yield return new WaitForSeconds(_visibilityDelay);
        //_textObj.gameObject.SetActive(true);
        print("trying to enable");
        Enable();


    }

    IEnumerator Enable()
    {
        //print("delaying");
        _textObj.gameObject.SetActive(false);
        yield return new WaitForSeconds(_visibilityDelay);

        //print("starting Enable method");
        if (_shouldFadeIn)
            _fadeIn = true;

        _textObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(_visibilityDuration);
        //_fadeIn = false;
        //print("done enabling, going to fade");

        if (!_followIndefinitely)
        {
            if (_shouldFadeOut)
            {
                _fadeIn = false;
                _fadeOut = true;
                //print("fade: " + _fade);
            }
            else
            {
                _textObj.gameObject.SetActive(false);
            }
        }
        
    }


}
