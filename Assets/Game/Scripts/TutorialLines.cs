using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLines : MonoBehaviour {

    public Transform[] _positions;

    public bool _fadeIn;
    public float _fadeInSpeed;

    public bool _fadeOut;
    public float _fadeOutSpeed;

    //public Transform _startTransform;
    //public Transform _endTransform;

    //public LineRenderer

    public LineRenderer _lineRendererObj;
    private Material _lineMat;

	// Use this for initialization
	void Start () {
        _lineRendererObj.positionCount = _positions.Length;
        _lineMat = _lineRendererObj.material;
	}
	
	// Update is called once per frame
	void Update () {

        for(int i = 0; i < _positions.Length; i++)
        {
            _lineRendererObj.SetPosition(i, _positions[i].position);
        }

        if (_fadeIn)
        {
            ObjectFade.LineRendererFadeIn(_lineRendererObj, 1f, _fadeInSpeed);
        }

	}

    public void FadeIn()
    {
        for (int i = 0; i < _positions.Length; i++)
        {
            _lineRendererObj.SetPosition(i, _positions[i].position);
        }
    }

    public void FadeOut()
    {

    }
}
