using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuText : MonoBehaviour {

    private TextMeshPro _textObj;

    public float _angleToViewMin;
    public float _angleToViewMax;

    public float _fadeSpeed;


	// Use this for initialization
	void Start () {
        _textObj = GetComponent<TextMeshPro>();
        _textObj.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        ObjectFade.TextLookAtFadeIn(_textObj, _angleToViewMin, _angleToViewMax, _fadeSpeed);
		
	}
}
