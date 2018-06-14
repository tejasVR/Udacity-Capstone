using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessControl : MonoBehaviour {

    public PostProcessVolume _postVolume;

    public static Vignette _postVignette;
    public static ColorGrading _postColorGrading;
    public static DepthOfField _postDOF;

    public PostProcessEffectSettings[] _postSettings;

    public float _vignetteStartValue;
    public float _contrastStartValue;
    public float _exposureStartValue;
    public float _dofFocalStartValue;

    public static float _exposureTargetValue;

    public float _vignetteFadeSpeed;
    public float _constrastFadeSpeed;
    public float _exposureFadeSpeed;
    public float _dofFocalFadeSpeed;

    private static bool _inventoryEffectEnabled;

    //[Header("Player Damage Effect Properties")]
    //public float _playerDamageVignetteAmount;
    //public float _playerDamageContrastAmount;
    

	// Use this for initialization
	void Start () {

        //_postVignette = _postVolume.

        _postVignette = ScriptableObject.CreateInstance<Vignette>();
        _postVignette.enabled.Override(true);
        _postVignette.intensity.Override(_vignetteStartValue);

        //_postSettings

        _postColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        _postColorGrading.enabled.Override(true);
        _postColorGrading.contrast.Override(_contrastStartValue);
        _postColorGrading.postExposure.Override(_exposureStartValue);

        _postDOF = ScriptableObject.CreateInstance<DepthOfField>();
        _postDOF.enabled.Override(true);
        _postDOF.focalLength.Override(_dofFocalStartValue);


        _postVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _postVignette, _postColorGrading, _postDOF);
    }

    // Update is called once per frame
    void Update () {

        if (_postVignette.intensity.value > _vignetteStartValue)
            _postVignette.intensity.value -= Time.deltaTime * _vignetteFadeSpeed;

        if (_postColorGrading.contrast.value > _contrastStartValue)
            _postColorGrading.contrast.value -= Time.deltaTime * _constrastFadeSpeed;

        //if (!_inventoryEffectEnabled)
        //{
        //    //if (_postDOF.focalLength.value > _dofFocalStartValue)
        //    //    _postDOF.focalLength.value -= Time.deltaTime * _dofFocalFadeSpeed;

        //    if (_postColorGrading.postExposure.value < _exposureStartValue)
        //        _postColorGrading.postExposure.value += Time.deltaTime * _exposureFadeSpeed;

        //}'

        if (_inventoryEffectEnabled)
        {
            if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureTargetValue) > .05f)
                _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureTargetValue, _exposureFadeSpeed);



        } else
        {
            if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureStartValue) > .05f)
                _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureStartValue, _exposureFadeSpeed);
        }


        //_postVignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
        //_postColorGrading.contrast.value = Mathf.Sin(Time.realtimeSinceStartup) * 30f;
    }

    public static void PlayerDamagePostEffect(float vignetteAmnt, float contrastAmnt)
    {
        _postVignette.intensity.value = vignetteAmnt;
        _postColorGrading.contrast.value = contrastAmnt;

        //print("damage effect enabled");
    }

    public static void InventoryOpenPostEffect(float exposureAmnt)
    {
        print("Invenory effect anabled");
        //_postDOF.focalLength.value = dofFocalLengthAmnt;
        _exposureTargetValue = exposureAmnt;
        _inventoryEffectEnabled = true;

    }

    public static void InventoryClosePostEffect()
    {
        _inventoryEffectEnabled = false;

        //_postDOF.focalLength.value = dofFocalLengthAmnt
    }

}
