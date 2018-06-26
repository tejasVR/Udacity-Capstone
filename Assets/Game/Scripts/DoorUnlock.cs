using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorUnlock : MonoBehaviour {

    //public bool isUnlocked;
    public GameObject _door;
    private HingeJoint _hinge;
    public string _keyToUnlock;
    private bool _isUnlocked;

    private Rigidbody _doorRb;

    public Transform _keyAttach;
    public RightControllerManager _rightControllerManager;

    private AudioSource _audioSource;
    public AudioClip[] _unlockSounds;

    public Image _lockImage;
    public TextMeshPro _doorText;
    //public GameObject _player;
    public float _distanceToShowLockImage;

    //public string 

	// Use this for initialization
	void Start () {
        _doorRb = _door.GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _hinge = _door.GetComponent<HingeJoint>();
        _hinge.useLimits = true;
        _doorRb.useGravity = false;
        _doorRb.isKinematic = true;
	}

    private void Update()
    {
        if (!_isUnlocked)
        {
            //print("calling update");
            ObjectFade.ImageFadeWithDistance(_lockImage, _distanceToShowLockImage, false);
            ObjectFade.TextFadeWithDistance(_doorText, _distanceToShowLockImage, false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if ((other.CompareTag("Collectable") || other.CompareTag("Collected")) && _keyToUnlock == other.GetComponent<Collectable>().itemName && !_isUnlocked)
        if (other.CompareTag("Collected") && _keyToUnlock == other.GetComponent<Collectable>().itemName && !_isUnlocked)
        {
            Unlock();
            //other.GetComponent<Light>().enabled = false;
            _rightControllerManager.AttachToDoor(_keyToUnlock, _keyAttach);
            //AttachToDoor(other.gameObject, other.GetComponent<Collectable>().itemName);
        }
    }

    public void Unlock()
    {
        PlaySound.PlayAudioFromSelection(_audioSource, _unlockSounds, true, -.05f, .05f);
        _isUnlocked = true;
        _hinge.useLimits = false;
        _doorRb.useGravity = true;
        _doorRb.isKinematic = false;
        _doorRb.angularVelocity = Vector3.zero;
        _lockImage.gameObject.SetActive(false);
        _doorText.gameObject.SetActive(false);
    }

    //public void AttachToDoor(GameObject key, string keyName)
    //{
    //    key.transform.position = _keyAttach.position;
    //    key.transform.parent = _keyAttach.transform;

    //    //rightControllerManager.GiveAwayItem(keyName);
    //}

}
