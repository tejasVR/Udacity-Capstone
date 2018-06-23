using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrigger : MonoBehaviour {

    public GameObject[] _objsToDisable;

    public Transform _raycastToObj;

    Vector3 _direction;

    public float _angleToDisable;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        _direction = _raycastToObj.transform.position - PlayerScript._playerEye.transform.position;

        if(!Physics.Raycast(PlayerScript._playerEye.transform.position, _direction))
        {
            if (Vector3.Angle(PlayerScript._playerEye.transform.forward, _direction) < _angleToDisable)
            {
                DisableObj();
            }
        }
		
	}


    private void DisableObj()
    {
        foreach (var obj in _objsToDisable)
        {
            obj.SetActive(false);
        }

        this.gameObject.SetActive(false);
    }

}
