using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Image _progressBarObj;

    public float _progressBarFillSpeed;

    private bool _isProgressBarFilled;

    //public GameManager _gameManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        ProgressBar();
		
	}

    private void ProgressBar()
    {
        if (PlayerScript._trackedRight.gameObject.activeInHierarchy && PlayerScript._trackedLeft.gameObject.activeInHierarchy)
        {
            if (_progressBarObj.fillAmount < 1)
            {
                if (PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.Trigger) || PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.Grip) ||
                PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu) ||
                PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Trigger) || PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Grip) ||
                PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
                {
                    _progressBarObj.fillAmount += Time.deltaTime * .5f;
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
