using System.Collections;
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_trackedObj.gameObject.activeInHierarchy)
        {
            _device = SteamVR_Controller.Input((int)_trackedObj.index);

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

        Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.green, .1f);
            
		
	}

    private void FixedUpdate()
    {
        if (this.GetComponent<Collectable>().isCollected)
        {
            // here just so we don't have to pick up the gun everytime
            //transform.position = _trackedObj.transform.position;
            //transform.rotation = _trackedObj.transform.rotation;
        }
        
    }

    public void Fire()
    {
        //print("Fired");
        muzzleFlash.Play();

        RaycastHit hit;
        Ray ray = new Ray(shootPoint.transform.position, shootPoint.transform.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            var hitPoint = hit.point;

            if (hit.collider.gameObject.tag == "Destructible")
            {
                Destructible destructible = hit.transform.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.DestroyIntoPieces();
                }

                Collider[] colliders = Physics.OverlapSphere(hitPoint, explosionRadius);
                foreach (var col in colliders)
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionForce, hitPoint, explosionRadius);
                    }
                }
            }

            print("Object Hit:" + hit.collider.gameObject.name);


            if (hit.collider.gameObject.tag == "Enemy")
            {
                print("Enemy Hit: " + hit.collider.gameObject.name);
                var hitDir = hit.point - shootPoint.transform.position;

                Enemy enemy = hit.collider.gameObject.transform.root.GetComponent<Enemy>();
                enemy.EnemyTakeHit(_damage, hitDir, hit.collider.gameObject);

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

            
        }
    }
}
