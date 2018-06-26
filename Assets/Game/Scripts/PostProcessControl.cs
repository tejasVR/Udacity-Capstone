using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessControl : MonoBehaviour {

    public PostProcessVolume _postVolume;

    public static Vignette _postVignette;
    public static ColorGrading _postColorGrading;
    public static DepthOfField _postDOF;

    public PostProcessEffectSettings[] _postSettings;

    //public float _vignetteStartValue;
    public float _vignetteOpactityStartValue;
    public float _contrastStartValue;
    public float _exposureStartValue;
    public float _focalLengthStartValue;
    public float _focusDisatnceStartValue;
    public float _redStartValue;

    public static float _exposureTargetValue;
    public static float _focalLengthTargetValue;
    public static float _focusDistanceTargetValue;
    public static float _redTargetValue;

    public float _vignetteFadeSpeed;
    public float _constrastFadeSpeed;
    public static float _exposureFadeSpeed;
    public static float _focalLengthFadeSpeed;
    public static float _focusDistanceFadeSpeed;
    public static float _redFadeSpeed;

    private static bool _inventoryEffectEnabled;

    //[Header("Player Damage Effect Properties")]
    //public float _playerDamageVignetteAmount;
    //public float _playerDamageContrastAmount;
    

	// Use this for initialization
	void Start () {

        //_postVignette = _postVolume.
        //_exposureFadeSpeed = .025f;

        _postVignette = ScriptableObject.CreateInstance<Vignette>();
        _postVignette.enabled.Override(true);
        //_postVignette.intensity.Override(_vignetteStartValue);
        _postVignette.opacity.Override(_vignetteOpactityStartValue);

        //_postSettings

        _postColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        _postColorGrading.enabled.Override(true);
        _postColorGrading.contrast.Override(_contrastStartValue);
        _postColorGrading.postExposure.Override(_exposureStartValue);
        //_postColorGrading.mixerGreenOutRedIn.Override(_redStartValue);

        //_postDOF = ScriptableObject.CreateInstance<DepthOfField>();
        //_postDOF.enabled.Override(true);
        //_postDOF.focalLength.Override(_focalLengthStartValue);
        //_postDOF.focusDistance.Override(_focusDisatnceStartValue);

        _postVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _postVignette, _postColorGrading);//, _postDOF);
    }

    // Update is called once per frame
    void Update () {

        //if (_postVignette.intensity.value > _vignetteStartValue)
        //    _postVignette.intensity.value -= Time.deltaTime * _vignetteFadeSpeed;

        if (_postVignette.opacity.value > _vignetteOpactityStartValue)
            _postVignette.opacity.value -= Time.deltaTime * _vignetteFadeSpeed;

        //if (_postColorGrading.contrast.value > _contrastStartValue)
        //    _postColorGrading.contrast.value -= Time.deltaTime * _constrastFadeSpeed;

        //if (!_inventoryEffectEnabled)
        //{
        //    //if (_postDOF.focalLength.value > _dofFocalStartValue)
        //    //    _postDOF.focalLength.value -= Time.deltaTime * _dofFocalFadeSpeed;

        //    if (_postColorGrading.postExposure.value < _exposureStartValue)
        //        _postColorGrading.postExposure.value += Time.deltaTime * _exposureFadeSpeed;

        //}'

        //if (_inventoryEffectEnabled)
        //{
        //    if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureTargetValue) > .05f)
        //        _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureTargetValue, _exposureFadeSpeed);
        //} else
        //{
        //    if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureStartValue) > .05f)
        //        _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureStartValue, _exposureFadeSpeed);
        //}


        //if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureTargetValue) > .05f)
        //    _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureTargetValue, _exposureFadeSpeed);
        //else if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureStartValue) > .05f)
        //    _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureStartValue, _exposureFadeSpeed);


        //if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureStartValue) > .05f)
        //    _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureStartValue, _exposureFadeSpeed);


        if (Mathf.Abs(_postColorGrading.postExposure.value - _exposureTargetValue) > .05f)
            _postColorGrading.postExposure.value = Mathf.Lerp(_postColorGrading.postExposure.value, _exposureTargetValue, _exposureFadeSpeed);

        //if (Mathf.Abs(_postDOF.focalLength.value - _focalLengthTargetValue) > .01f)
        //    _postDOF.focalLength.value = Mathf.Lerp(_postDOF.focalLength.value, _focalLengthTargetValue, _focalLengthFadeSpeed);

        //if (Mathf.Abs(_postDOF.focusDistance.value - _focusDistanceTargetValue) > .01f)
        //    _postDOF.focusDistance.value = Mathf.Lerp(_postDOF.focusDistance.value, _focusDistanceTargetValue, _focusDistanceFadeSpeed);
        //print(_postDOF.focusDistance.value)

        //_postVignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
        //_postColorGrading.contrast.value = Mathf.Sin(Time.realtimeSinceStartup) * 30f;
    }

    public static void PlayerDamagePostEffect(float vignetteAmnt, float contrastAmnt)
    {
        //_postVignette.intensity.value = vignetteAmnt;
        _postVignette.opacity.value = vignetteAmnt;
        //_postColorGrading.contrast.value = contrastAmnt;

        //print("damage effect enabled");
    }

    public static void PostExposureFade(float exposureTarget, float exposureFadeSpeed)
    {
        //print("fade effect anabled");
        //_postDOF.focalLength.value = dofFocalLengthAmnt;
        //_postColorGrading.postExposure.value = exposureAmtBegin;
        _exposureTargetValue = exposureTarget;
        _exposureFadeSpeed = exposureFadeSpeed;
        //_inventoryEffectEnabled = true;

    }

    public static void InventoryClosePostEffect()
    {
        //_inventoryEffectEnabled = false;

        //_postDOF.focalLength.value = dofFocalLengthAmnt
    }

    public static void InventoryOpenPostEffect()
    {
        //_inventoryEffectEnabled = true;

        //_postDOF.focalLength.value = dofFocalLengthAmnt
    }

    public static void DOFFade(float focalLengthTarget, float focusDistanceTarget, float focalLengthFadeSpeed, float focusDistanceFadeSpeed)
    {
        //_postDOF.focalLength.value = focalLengthAmtBegin;

        _focalLengthTargetValue = focalLengthTarget;
        _focalLengthFadeSpeed = focusDistanceFadeSpeed;

        _focusDistanceTargetValue = focusDistanceTarget;
        _focusDistanceFadeSpeed = focusDistanceFadeSpeed;

        

    }

    public static void OpeningFadeEffect(float exposureAmtBegin, float exposureAmtEnd, float exposureFadeSpeed)
    {
        //print("fade effect anabled");
        //_postDOF.focalLength.value = dofFocalLengthAmnt;
        _postColorGrading.postExposure.value = exposureAmtBegin;
        _exposureTargetValue = exposureAmtEnd;
        _exposureFadeSpeed = exposureFadeSpeed;
        //_inventoryEffectEnabled = true;

    }



}
