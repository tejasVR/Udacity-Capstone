using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public SteamVR_TrackedObject _trackedObj;
    private SteamVR_Controller.Device _device;

    public GameObject shootPoint;

    public ParticleSystem muzzleFlash;

    public float _damage = 10f;
    public float _range = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_trackedObj.gameObject.activeInHierarchy)
        {
            _device = SteamVR_Controller.Input((int)_trackedObj.index);

            if (_device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Fire();
            }
        }
            
		
	}

    private void FixedUpdate()
    {
        // here just so we don't have to pick up the gun everytime
        transform.position = _trackedObj.transform.position;
        transform.rotation = _trackedObj.transform.rotation;
    }

    public void Fire()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            Destructible destructible = hit.transform.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.DestroyIntoPieces();
            }
        }
    }
}
