using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Image _progressBarObj;

    public float _progressBarFillSpeed;

    private bool _isProgressBarFilled;

    private float _hapticClick = .5f;
    private float _hapticClickTimer;

    //public GameManager _gameManager;

    // Use this for initialization
    void Start () {
        _hapticClickTimer = _hapticClick;
	}
	
	// Update is called once per frame
	void Update () {

        ProgressBar();
		
	}

    private void ProgressBar()
    {
        if (PlayerScript._dominantHand.gameObject.activeInHierarchy && PlayerScript._nonDominnatHand.gameObject.activeInHierarchy)
        {
            if (_progressBarObj.fillAmount < 1)
            {
                if (PlayerScript._deviceDominant.GetPress(SteamVR_Controller.ButtonMask.Trigger) || PlayerScript._deviceDominant.GetPress(SteamVR_Controller.ButtonMask.Grip) ||
                PlayerScript._deviceDominant.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || PlayerScript._deviceDominant.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu) ||
                PlayerScript._deviceNonDominant.GetPress(SteamVR_Controller.ButtonMask.Trigger) || PlayerScript._deviceNonDominant.GetPress(SteamVR_Controller.ButtonMask.Grip) ||
                PlayerScript._deviceNonDominant.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || PlayerScript._deviceNonDominant.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
                {
                    _progressBarObj.fillAmount += Time.deltaTime * .5f;

                    if (_hapticClickTimer > 0)
                        _hapticClickTimer -= Time.deltaTime;
                    else
                    {
                        HapticFeedback.HapticAmount(500, true, true);
                    }
                }
                else
                {
                    if (_progressBarObj.fillAmount > 0)
                        _progressBarObj.fillAmount -= Time.deltaTime * .25f;
                    if (_progressBarObj.fillAmount <= 0)
                        _progressBarObj.fillAmount = 0;
                }
            }
            else
            {
                if (!_isProgressBarFilled)
                {
                    ProgressBarFilled();
                    _isProgressBarFilled = true;
                }
            }
        }       
    }

    private void ProgressBarFilled()
    {
        GameManager.NextScene();
        this.gameObject.SetActive(false);
    }
}
