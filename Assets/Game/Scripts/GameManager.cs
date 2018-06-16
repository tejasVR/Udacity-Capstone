using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {

    //public bool dominantLeft;
    //public bool dominantRight;

    public bool _thisIsTheIntroScene;
    public bool _thisIsTheMainScene;

    public float _introSceneTimer;

    //public GameObject _playerIntro;
    //public GameObject _playerMain;

    public GameObject _mainSceneLoad;

    //public GameObject introEnvironment;

    //public GameObject _escapeHouseText;


    private void Awake()
    {
        //XRSettings.eyeTextureResolutionScale = 1.8f;
    }

    // Use this for initialization
    void Start () {
        if (_thisIsTheIntroScene)
            StartCoroutine(IntroTimer());

        if(_thisIsTheMainScene)
            StartCoroutine(MainSceneStart());
        
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

    private void DeacivateIntro()
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

    private IEnumerator IntroTimer()
    {
        yield return new WaitForSeconds(_introSceneTimer);
        DeacivateIntro();
    }

    private IEnumerator MainSceneStart()
    {
        yield return new WaitForSeconds(.05f);
        //DeacivateIntro();
        PostProcessControl.OpeningFadeEffect(-10);
        //_escapeHouseText.SetActive(true);
    }
}
