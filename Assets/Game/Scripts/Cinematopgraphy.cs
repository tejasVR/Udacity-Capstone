using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cinematopgraphy : MonoBehaviour {

    [SerializeField]
    #region Camera Action Variables
    public bool _cameraPan;
    public bool _cameraTilt;
    public bool _cameraDolly;
    public bool _cameraTrack;
    public bool _cameraRotate;
    public bool _cameraPedestal;
    #endregion

    #region Camera Speed Variables
    public float _cameraPanSpeed;
    public float _cameraTiltSpeed;
    public float _cameraDollySpeed;
    public float _cameraTrackSpeed;
    public float _cameraRotateSpeed;
    public float _cameraPedestalSpeed;
    #endregion

    #region PublicEnum
    public PanDirection _panDirection;
    public TiltDirection _tiltDirection;
    public DollyDirection _dollyDirection;
    public TrackDirection _trackDirection;
    public RotateDirection _rotateDirection;
    public PedestalDirection _pedestalDirection;
    #endregion

    #region EnumValues
    public enum PanDirection { PanLeft, PanRight }
    public enum TiltDirection { TitlUp, TiltDown }
    public enum DollyDirection { DollyForward, DollyBackward }
    public enum TrackDirection { TrackLeft, TrackRight }
    public enum RotateDirection { RotateLeft, RotateRight }
    public enum PedestalDirection { PedestalUp, PedestalDown }
    #endregion

    
	// Update is called once per frame
	void Update () {

        if (_cameraDolly)
        {
            transform.Translate(Vector3.forward * _cameraDollySpeed * Time.deltaTime);
        }
		
	}
}
