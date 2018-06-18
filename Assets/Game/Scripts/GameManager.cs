using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {

    //public bool dominantLeft;
    //public bool dominantRight;

    //public Image _progressBarObj;
    public static GameManager instance;

    public GameObject _introEnvironmentObj;
    public static GameObject _introEnvironment;

    public float _introSceneTimerCounter;
    public static float _introSceneTimer;


    public bool _thisIsIntro;
    public bool _thisIsMain;
    public bool _thisIsEnd;

    public static bool _thisIsTheIntroScene;
    public static bool _thisIsTheMainScene;
    public static bool _thisIsTheEndScene;


    //public GameObject _playerIntro;
    //public GameObject _playerMain;
    public GameObject _introSceneLoadObj;
    public GameObject _mainSceneLoadObj;
    public GameObject _endSceneLoadObj;

    public static GameObject _introSceneLoad;
    public static GameObject _mainSceneLoad;
    public static GameObject _endSceneLoad;

    //public GameObject _menuObj;

    //public GameObject _escapeHouseText;


    private void Awake()
    {
        _introEnvironment = _introEnvironmentObj;
        _introSceneTimer = _introSceneTimerCounter;

        _introSceneLoad = _introSceneLoadObj;
        _mainSceneLoad = _mainSceneLoadObj;
        _endSceneLoad = _endSceneLoadObj;

        _thisIsTheIntroScene = _thisIsIntro;
        _thisIsTheMainScene = _thisIsMain;
        _thisIsTheEndScene = _thisIsEnd;
        //XRSettings.eyeTextureResolutionScale = 1.8f;

        if (_thisIsTheIntroScene)
            _introEnvironment.SetActive(false);

        instance = this;

    }

    // Use this for initialization
    void Start() {

        //if (_thisIsTheIntroScene)
        //    introEnvironment.SetActive(false);

        //if (_thisIsTheMainScene || _thisIsTheEndScene)
            StartCoroutine(GameFade());
        
    }


    private void Update()
    {

       
        //if (_thisIsTheIntroScene)
        //    ProgressBar();
    }

    //// Update is called once per frame
    //void Update () {
    //       //if (_thisIsTheIntroScene)
    //       {
    //           if (_introSceneTimer > 0)
    //           {
    //               _introSceneTimer -= Time.deltaTime;
    //               if (_introSceneTimer <= 0)
    //               {
    //                   //SteamVR_LoadLevel.Begin(SceneManager.GetActiveScene().buildIndex + 1.ToString);
    //                   //SteamVR_LoadLevel.Begin(SceneManager.GetSceneByBuildIndex(1).name);
    //                   //SteamVR_LoadLevel.Begin("Main Scene v.13");
    //                   //SteamVR_LoadLevel.Begin()
    //                   //SceneManager.LoadScene(1, LoadSceneMode.Additive);
    //                   //print("loading level");
    //                   DeacivateIntro();
    //               }
    //           }
    //       }
    //   }

    public static void DeacivateIntro()
    {
        //_playerMain.SetActive(true);
        //SteamVR_LoadLevel.Begin("Main Scene v.13", false, 2f, 0, 0, 0, 0);
        //SteamVR_LoadLevel.
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);

        _mainSceneLoad.SetActive(true);

    }

    private void SetupIntro()
    {
        //_playerIntro.SetActive(true);
        //_playerMain.SetActive(false);
    }

    public static void NextScene()
    {
        print("calling start intro method");
        if(_thisIsTheIntroScene)
            instance.StartCoroutine(StartIntro());

        if (_thisIsTheMainScene)
            instance.StartCoroutine(MoveToEndScene());

        if (_thisIsTheEndScene)
            instance.StartCoroutine(MoveToIntroScene());
    }

    public static IEnumerator StartIntro()
    {
        print("calling start intro co routine");
        _introEnvironment.SetActive(true);
        yield return new WaitForSeconds(_introSceneTimer);
        _mainSceneLoad.SetActive(true);
        PostProcessControl.OpeningFadeEffect(-10, 0, .025f);
        //DeacivateIntro();
    }

    public static IEnumerator MoveToEndScene()
    {
        yield return new WaitForSeconds(.05f);
        //DeacivateIntro();
        //PostProcessControl.PostExposureFade(-10, 0, .025f);
        //_escapeHouseText.SetActive(true);
        _endSceneLoad.SetActive(true);
    }

    public static IEnumerator MoveToIntroScene()
    {
        yield return new WaitForSeconds(.05f);
        //DeacivateIntro();
        //PostProcessControl.PostExposureFade(-10, 0, .025f);
        //_escapeHouseText.SetActive(true);
        _introSceneLoad.SetActive(true);
    }

    public static IEnumerator GameFade()
    {
        yield return new WaitForSeconds(.05f);
        //DeacivateIntro();
        PostProcessControl.OpeningFadeEffect(-10, 0, .02f);
    }



    //private void ProgressBar()
    //{
    //    if (_progressBarObj.fillAmount < 1)
    //    {
    //        if (PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.Trigger) || PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.Grip) ||
    //        PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu) ||
    //        PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Trigger) || PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Grip) ||
    //        PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || PlayerScript._deviceLeft.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
    //        {
    //            _progressBarObj.fillAmount += Time.deltaTime * .5f;
    //        }
    //        else
    //        {
    //            if (_progressBarObj.fillAmount > 0)
    //                _progressBarObj.fillAmount -= Time.deltaTime * .25f;
    //            if (_progressBarObj.fillAmount <= 0)
    //                _progressBarObj.fillAmount = 0;
    //        }
    //    }
    //    else
    //    {
    //        if (!_isProgressBarFilled)
    //        {
    //            ProgressBarFilled();
    //            _isProgressBarFilled = true;

    //        }

    //    }
    //}


}
