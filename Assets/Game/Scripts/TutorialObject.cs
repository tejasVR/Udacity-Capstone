using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialObject : MonoBehaviour {

    //[Header("Text Booleans")]
    //public bool _isMoveWithTriggerText;

    public enum TutorialTextObj
    {
        PressTriggerToMove,
        PressTriggerToPickUpItem,
        PressTouchPadToBringUpInventory
    }

    public TutorialTextObj _tutorialText;

    public TextMeshPro _textObj;
    
    [Space(10)]

    public bool _hasLine;
    public bool _fadeInLine;
    public LineRenderer _lineRendererObj;
    public Transform[] _linePositions;
    public float _lineFadeInDelay;

    public float _fadeOutSpeed;

    private bool _endTutorialObject;

    public bool _shouldDisableAfterEnd;
    public float _disableObjCounter;

    public RightControllerManager _rightControllerManager;
    //public SteamVR_TrackedObject _trackedRight;
    //public SteamVR_TrackedObject _trackedLeft;

    //private SteamVR_Controller.Device _deviceRight;
    //private SteamVR_Controller.Device _deviceLeft;

    //private float _triggerAxisRight;


	// Use this for initialization
	void Start () {
        if (_hasLine && _fadeInLine)
            StartCoroutine(LineFade());
	}
	
	// Update is called once per frame
	void Update () {

        if (_hasLine)
        {
            for (int i = 0; i < _linePositions.Length; i++)
            {
                _lineRendererObj.SetPosition(i, _linePositions[i].position);
            }
        }

       //_deviceRight = SteamVR_Controller.Input((int)_trackedRight.index);
       //_deviceLeft = SteamVR_Controller.Input((int)_trackedLeft.index);
       if (!_endTutorialObject)
        {
            if (_tutorialText == TutorialTextObj.PressTriggerToMove)
            {
                if (PlayerScript._triggerAxisLeft > .5)
                {
                    _endTutorialObject = true;
                }
            }
            else if (_tutorialText == TutorialTextObj.PressTriggerToPickUpItem)
            {
                ObjectFade.TextFadeWithDistance(_textObj, 1.4f, false);

                if (PlayerScript._trackedRight.gameObject.activeInHierarchy)
                {
                    if (PlayerScript._deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        foreach (var item in _rightControllerManager._inventorySlots)
                        {
                            if (item.name == "Attic Key")
                            {
                                _endTutorialObject = true;
                            }
                        }
                    }
                }

                
            }
            else if (_tutorialText == TutorialTextObj.PressTouchPadToBringUpInventory)
            {
                if (PlayerScript._deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    _endTutorialObject = true;
                }
            }
        }
      



        if (_endTutorialObject)
        {
            EndTutorialObject();

            if (_shouldDisableAfterEnd)
            {
                _disableObjCounter -= Time.deltaTime;
                if (_disableObjCounter <= 0)
                    this.gameObject.SetActive(false);
            }
        }

        if (_fadeInLine && !_endTutorialObject)
        {
            ObjectFade.LineRendererFadeIn(_lineRendererObj, 1f, _fadeOutSpeed);
        }

    }

    public void EndTutorialObject()
    {
       
        ObjectFade.TextFadeOut(_textObj, _fadeOutSpeed, true);

        if (_hasLine)
        {
            //_fadeInLine = false;
            ObjectFade.LineRendererFadeOut(_lineRendererObj, _fadeOutSpeed, true);
        }
    }

    IEnumerator LineFade()
    {
        _lineRendererObj.gameObject.SetActive(false);
        yield return new WaitForSeconds(_lineFadeInDelay);
        _lineRendererObj.gameObject.SetActive(true);
        _fadeInLine = true;
    }
}
