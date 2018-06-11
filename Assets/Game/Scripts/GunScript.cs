﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public SteamVR_TrackedObject _trackedObj;
    private SteamVR_Controller.Device _device;

    public GameObject shootPoint;

    public ParticleSystem muzzleFlash;

    public float explosionForce;
    public float explosionRadius;

    public float _damage = 1f;
    public float _range = 100f;

    public float fireRate = 1f;
    private float fireRateTimer = 1f;

    public bool firstPickedUp;

    private Rigidbody rb;

    private int layerMask = ~0;

    public float deviceAngularVelocity;

    public float recoilZAngle = 37f;
    public GameObject gunBody;
    private Quaternion gunBodyBaseRotation;

    public float gunRecoilAngleSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        gunBodyBaseRotation = gunBody.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (_trackedObj.gameObject.activeInHierarchy)
        {
            _device = SteamVR_Controller.Input((int)_trackedObj.index);

            deviceAngularVelocity = _device.angularVelocity.magnitude;

            if (_device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && firstPickedUp && fireRateTimer == fireRate)
            {
                Fire();
                fireRateTimer = 0f;
            }
        }

        if (this.GetComponent<Collectable>().isCollected && !firstPickedUp)
        {
            firstPickedUp = true;
        }

        if (fireRateTimer < fireRate)
        {
            fireRateTimer += Time.deltaTime;
        } else
        {
            fireRateTimer = fireRate;
        }

        //Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.green, .1f);
        Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.green);

        gunBody.transform.localRotation = Quaternion.Lerp(gunBody.transform.localRotation, gunBodyBaseRotation, Time.deltaTime * gunRecoilAngleSpeed);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destructible")
        {
            if(deviceAngularVelocity > 5f)
            {
                Destructible destructible = other.transform.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.DestroyIntoPieces();
                }

                AddExplosionForce(transform.position, explosionRadius, -explosionForce);
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.GetComponent<Collectable>().isCollected)
        {
            // here just so we don't have to pick up the gun everytime
            //transform.position = _trackedObj.transform.position;
            //transform.rotation = _trackedObj.transform.rotation;
        }

        //print(rb.angularVelocity.normalized);

        //if (transform.parent.gameObject.GetComponent<Rigidbody>() != null)
        //    print(transform.parent.gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude);




    }

    public void Fire()
    {
        //print("Fired");
        muzzleFlash.Play();



        RaycastHit hit;
        Ray ray = new Ray(shootPoint.transform.position, shootPoint.transform.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            var hitPoint = hit.point;

            if (hit.collider.gameObject.tag == "Destructible")
            {
                Destructible destructible = hit.transform.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.DestroyIntoPieces();
                }

                AddExplosionForce(hit.point, explosionRadius, explosionForce);
            }

            print("Object Hit:" + hit.collider.gameObject.name);


            if (hit.collider.gameObject.tag == "Enemy")
            {
                //print("Enemy Hit: " + hit.collider.gameObject.name);

                //var hitDir = shootPoint.transform.position - hit.point;
                var hitDir = shootPoint.transform.forward;

                var parentObj = hit.collider.transform.parent;

                while (parentObj.GetComponent<Enemy>() == null)
                {
                    parentObj = parentObj.transform.parent;
                    //print("Going through while loop. Current Parent:" + parentObj.name);
                }

                //print("Enemy Hit: " + parentObj.name);


                Enemy enemy = parentObj.GetComponent<Enemy>();
                enemy.EnemyTakeHit(_damage, hitDir, hit.collider.gameObject);

                AddExplosionForce(hit.point, explosionRadius, explosionForce);


                //Collider[] colliders = Physics.OverlapSphere(hitPoint, 5f);
                //foreach (var col in colliders)
                //{
                //    print(col.gameObject.name);
                //}
                //print(colliders[0].gameObject.name);

                //if (colliders[0].transform.root.GetComponent<Enemy>() != null)
                //{
                //    colliders[0].transform.root.GetComponent<Enemy>().EnemyTakeHit(_damage);
                //    print("Shot enemy - damage:" + _damage);
                //}

                //Enemy enemy = hit.transform.root.GetComponent<Enemy>();
                //enemy.EnemyTakeHit(_damage);

            }



            //Debug.Log(hit.transform.name);

            gunBody.transform.localRotation = Quaternion.Euler(gunBody.transform.localRotation.x, gunBody.transform.localRotation.y, gunBody.transform.localRotation.z + recoilZAngle);

        }
    }

    private void AddExplosionForce(Vector3 point, float explosionRadius, float explosionForce)
    {
        Collider[] colliders = Physics.OverlapSphere(point, explosionRadius);
        foreach (var col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, point, explosionRadius);
            }
        }
    }
}
