using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessControl : MonoBehaviour {

    public PostProcessVolume _postVolume;

    public static Vignette _postVignette;
    public static ColorGrading _postColorGrading;

    public float _vignetteFadeOutSpeed;
    public float _constrastFadeOutSpeed;

	// Use this for initialization
	void Start () {

        //_postVignette = _postVolume.

        _postVignette = ScriptableObject.CreateInstance<Vignette>();
        _postVignette.enabled.Override(true);

        //_postColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        //_postColorGrading.enabled.Override(true);

        _postVignette.intensity.Override(0f);

        _postVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _postVignette);//, _postColorGrading);
    }

    // Update is called once per frame
    void Update () {

        if (_postVignette.intensity.value > .3f)
            _postVignette.intensity.value -= Time.deltaTime * _vignetteFadeOutSpeed;

        //if (_postColorGrading.contrast.value > 0)
        //    _postColorGrading.contrast.value -= Time.deltaTime * _constrastFadeOutSpeed;

        //_postVignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
    }

    public static void PlayerDamageEffect()
    {
        _postVignette.intensity.value = 1f;
       // _postColorGrading.contrast.value = 30f;

        print("damage effect enabled");
    }

}
