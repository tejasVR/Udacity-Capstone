using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour {

    public PlayerScript _playerScript;

    public bool _isSettingMenuOpen;
    public bool _firstPressUp;

    public GameObject _settingMenuObj;
    public GameObject _cursorObj;

    public int _currentItem;
    public int _oldItem;

    private float _angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public List<OptionsSlot> _optionsSlots = new List<OptionsSlot>();

    public Color _idleUIColor;
    public Color _highlightedUIColor;

    public bool _rightDominant = true;
    
    //For graphics settings, 0 = low, 1 = medium, 2 = high
    public int _graphicsSettingsInt = 1;
    string[] _graphicsSettingsNames;

    // Use this for initialization
    void Start () {
        _settingMenuObj.SetActive(false);
        _graphicsSettingsNames = QualitySettings.names;

    }

    // Update is called once per frame
    void Update () {

        if (PlayerScript._deviceNonDominant.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !_isSettingMenuOpen)
        {
            _isSettingMenuOpen = true;
            _settingMenuObj.SetActive(true);
            HapticFeedback.HapticAmount(500, false, true);

        }

        if (_isSettingMenuOpen)
        {
            OpenSettings();
        }

    }

    private void OpenSettings()
    {
        TouchpadEnabled();

        if (PlayerScript._deviceNonDominant.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && !_firstPressUp)
            _firstPressUp = true;

        if (PlayerScript._touchpadNonDominant.magnitude > .25f)
        {
            // Option Slot #1 - Resume
            if (_angleFromCenter > 210 && _angleFromCenter < 260)
                _currentItem = 0;
            
            // Option Slot #2 - Switch Hands
            else if (_angleFromCenter > 285 && _angleFromCenter < 335)
                _currentItem = 1;

            // Option Slot #3 - Graphics Settings
            else if (_angleFromCenter > 25 && _angleFromCenter < 75)
                _currentItem = 2;

            // Option Slot #3 - Quit
            else if (_angleFromCenter > 110 && _angleFromCenter < 160)
                _currentItem = 3;

            // No Option is selected
            else
                _currentItem = -1;
        }
        else
            _currentItem = -1;


        if (_currentItem != _oldItem)
        {
            if (_currentItem > -1)
            {
                foreach (var slot in _optionsSlots)
                {
                    slot.optionOutline.color = _idleUIColor;
                    slot.optionValue.color = _idleUIColor;

                    if (slot.descriptionText != null)
                        slot.descriptionText.gameObject.SetActive(false);
                }

                _optionsSlots[_currentItem].optionOutline.color = _highlightedUIColor;
                _optionsSlots[_currentItem].optionValue.color = _highlightedUIColor;

                if (_optionsSlots[_currentItem].descriptionText != null)
                    _optionsSlots[_currentItem].descriptionText.gameObject.SetActive(true);

                HapticFeedback.HapticAmount(500, false, true);

            }
            else
            {
                foreach (var slot in _optionsSlots)
                {
                    slot.optionOutline.color = _idleUIColor;
                    if (slot.descriptionText != null)
                        slot.descriptionText.gameObject.SetActive(false);
                }
            }

            _oldItem = _currentItem;
        }

        if (PlayerScript._deviceNonDominant.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && _currentItem > -1 && _firstPressUp)
        {
            switch (_currentItem)
            {
                case 0:
                    CloseInventory();
                    break;
                case 1:
                    SwitchHands();
                    break;
                case 2:
                    SwitchGraphics();
                    break;
                case 3:
                    QuitApplication();
                    break;


            }


            //if (_optionsSlots[_currentItem]. != null)
            {
                //PutItemInHand(_inventorySlots[_currentItem]);
                HapticFeedback.HapticAmount(750, false, true);

                //_inventorySlots[_currentItem].hasItemInHand = true;
                //_handModelObj.SetActive(false);
                CloseInventory();
            }



            //if (_inventorySlots[_currentItem].itemInHandObj != null)
            //{
            //    _inventorySlots[_currentItem].itemInHandObj.SetActive(true);
            //    //objInHand.transform.position = transform.position;
            //    //objInHand = itemList[currentItem].itemInHandObj;
            //    //objInHand.SetActive(true);
            //    _hasItemInHand = true;
            //}






        }

        if (PlayerScript._deviceNonDominant.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && _firstPressUp & _currentItem == -1)
            CloseInventory();

    }

    public void CloseInventory()
    {
        //PostProcessControl.PostExposureFade(_exposureInventoryOff, _exposureFadeSpeed);
        //PostProcessControl.DOFFade(_focalLengthInventoryOff, _focusDistanceInventoryOff, _focalLengthFadeSpeed, _focusDistanceFadeSpeed);

        //print("closing inventory");
        _isSettingMenuOpen = false;
        _settingMenuObj.SetActive(false);
        //PlaceItemsInInventory(true, false);
        //ShowInventoryItems(false);
        _firstPressUp = false;
    }


    private void TouchpadEnabled()
    {
        _cursorObj.SetActive(true);

        // if the menu is open, get both the x and y values of the touchpad
        //_touchpad.x = PlayerScript._deviceRight.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        //_touchpad.y = PlayerScript._deviceRight.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        _cursorObj.transform.localPosition = Vector3.Lerp(_cursorObj.transform.localPosition, PlayerScript._touchpadNonDominant * .12f, Time.unscaledDeltaTime * 10f);

        Vector2 fromVector2 = new Vector2(0, 1);
        Vector2 toVector2 = PlayerScript._touchpadNonDominant;

        // Measure the users' thumb angle in relation to the center of the touchpad.
        _angleFromCenter = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        // This will get the angle of the users' thumb so we know what inventory item they are trying to highlight
        if (cross.z > 0)
        {
            _angleFromCenter = 360 - _angleFromCenter;
        }

    }

    private void SwitchHands()
    {
        if (_rightDominant)
        {
            _optionsSlots[_currentItem].optionValue.text = "LEFT";
            _playerScript.SwitchHands();

            _rightDominant = false;
        }
        else
        {
            _optionsSlots[_currentItem].optionValue.text = "RIGHT";
            _playerScript.SwitchHands();

            _rightDominant = true;
        }

        CloseInventory();
    }

    private void SwitchGraphics()
    {
        _graphicsSettingsInt++;
        if (_graphicsSettingsInt > _graphicsSettingsNames.Length - 1)
            _graphicsSettingsInt = 0;

        QualitySettings.SetQualityLevel(_graphicsSettingsInt, true);

        switch (_graphicsSettingsInt)
        {
            case 0:
                _optionsSlots[_currentItem].optionValue.text = "LOW";
                break;
            case 1:
                _optionsSlots[_currentItem].optionValue.text = "MEDIUM";
                break;
            case 2:
                _optionsSlots[_currentItem].optionValue.text = "HIGH";
                break;
        }

        //for (int i = 0; i < _graphicsSettingsNames.Length; i++)
        //{
        //    print(QualitySettings.names[i]);
        //}

    }

    private void QuitApplication()
    {
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    [System.Serializable]
    public class OptionsSlot
    {
        public TextMeshPro optionValue;
        public GameObject descriptionText;
        public Image optionOutline;
    }
}
