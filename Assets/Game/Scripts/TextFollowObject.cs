using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFollowObject : MonoBehaviour {

    //public bool _shouldDelay;
    public float _visibilityDelay;

    [Space(10)]

    public float _visibilityDuration;

    [Space(10)]

    public bool _shouldMoveWithCamera;
    public float _moveSpeed;

    [Space(10)]

    public bool _shouldFadeOut;
    public float _fadeOutSpeed;

    [Space(10)]

    public bool _shouldDisable;
    private float _disableObjCounter;

    [Space(10)]


    public int _distanceFromCamera;

    public GameObject _followObject;
    public TextMeshPro _textObj;

    private bool _fadeIn;

	// Use this for initialization
	void Start () {
        if (_shouldDisable)
        StartCoroutine(Disable());

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

        if (_fadeIn)
        {
            ObjectFade.TextFadeOut(_textObj, _fadeOutSpeed, true);
            //print("trying to fade");
        }

        if (_shouldMoveWithCamera)// && _textObj.gameObject.activeInHierarchy)
        {
            GetPos();
        }
        
		
	}

    private void GetPos()
    {
        Ray ray = new Ray(_followObject.transform.position, _followObject.transform.forward);
        var point = ray.GetPoint(_distanceFromCamera);

        transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime * _moveSpeed);
        transform.LookAt(_followObject.transform.position);
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
        _textObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(_visibilityDuration);
        //print("done enabling, going to fade");

        if (_shouldFadeOut)
        {

            _fadeIn = true;
            //print("fade: " + _fade);
        }
        else
        {
            _textObj.gameObject.SetActive(false);
        }
    }


}
