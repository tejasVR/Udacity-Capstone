using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour {

    public Transform _startPos;
    public Transform _currentPos;
    public Transform _player;
    public Transform _playerArea;

    public AudioSource _audioSource;
    public AudioSource _audioSourceBackup;

    public AudioClip[] _stepsWood1;
    public AudioClip[] _stepsWood2;
    public AudioClip[] _stepsWood3;
    public AudioClip[] _stepsConcrete;
    public AudioClip[] _stepsStairs;
    public AudioClip[] _stepsLeafs;

    private float _distanceFromStartPos;

    public float _distanceToStepNormal;
    public float _distanceToStepStairs;
    private float _distanceToStep;

    public bool _alterPitch;


    public float heightOffGround;

    private int layerMask = 1 << 18;

    //public bool ifStep;

    // Use this for initialization
    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
        _startPos.transform.position = _currentPos.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        _currentPos.transform.position = new Vector3(_player.transform.position.x, heightOffGround + _playerArea.transform.position.y, _player.transform.position.z);
        _distanceFromStartPos = Vector3.Distance(_currentPos.transform.position, _startPos.transform.position);

        if (_distanceFromStartPos > _distanceToStep)
        {
            TakeStep();
            HapticFeedback.HapticAmount(500, false, true);
            //print("Taking step");
        }
    }

    private void TakeStep()
    {
        _startPos.transform.position = new Vector3(_currentPos.transform.position.x, heightOffGround + _playerArea.transform.position.y, _currentPos.transform.position.z);

        Ray ray = new Ray(_startPos.transform.position, Vector3.down);
        RaycastHit hit;
        Debug.DrawRay(_startPos.transform.position, Vector3.down, Color.green, 10f);

        if (Physics.Raycast(ray, out hit, 5f, layerMask))
        {
            //print("hi there");
            //print(hit.transform.gameObject.tag);
            switch (hit.transform.gameObject.tag)
            {
                case "Wood1":
                    RandomSound(_stepsWood1);
                    break;
                case "Wood2":
                    RandomSound(_stepsWood2);
                    break;
                case "Wood3":
                    RandomSound(_stepsWood3);
                    break;
                case "Concrete":
                    RandomSound(_stepsConcrete);
                    break;
                case "Stairs":
                    RandomSound(_stepsStairs);
                    break;
                case "Leafs":
                    RandomSound(_stepsLeafs);
                    break;
            }

            if (hit.transform.CompareTag("Stairs"))
                _distanceToStep = _distanceToStepStairs;
            else
                _distanceToStep = _distanceToStepNormal;
        }
    }

    private void RandomSound(AudioClip[] clips)
    {
        var randomInt = Random.Range(0, clips.Length - 1);

        var source = new AudioSource();

        if (_audioSource.isPlaying)
            source = _audioSourceBackup;
        else
            source = _audioSource;

        source.clip = clips[randomInt];

        if (_alterPitch)
            source.pitch = 1 + Random.Range(-.15f, .15f);

        source.Play();
    }
}
