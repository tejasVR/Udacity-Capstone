using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    //public SteamVR_TrackedObject _trackedObj;
    //private SteamVR_Controller.Device _device;

    private Collectable _collectable;
    public GameObject shootPoint;
    public GameObject _gunShotTrailPrefab;

    public ParticleSystem muzzleFlash;
    //public ParticleSystem shotTrail;

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

    //public GlobalAudio globalLowPass;
    public AudioSource[] _audioSource;
    //public AudioClip[] clips;

    public Light[] _lights;

    private bool clickSoundPlayed = true;

    public string _previousTag;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        gunBodyBaseRotation = gunBody.transform.localRotation;
        _previousTag = this.gameObject.tag;
        _collectable = GetComponent<Collectable>();
	}

    private void OnEnable()
    {
        //print("OnEnable Called");
        //if (this.gameObject.tag == "Collected")
        //{
        //    foreach (var light in _lights)
        //    {
        //        light.enabled = true;
        //    }
        //} else
        //{
        //    foreach (var light in _lights)
        //    {
        //        light.enabled = false;
        //    }
        //}
            
    }

    // Update is called once per frame
    void Update () {
        if (PlayerScript._dominantHand.gameObject.activeInHierarchy)
        {
            //_device = SteamVR_Controller.Input((int)_trackedObj.index);

            deviceAngularVelocity = PlayerScript._deviceDominant.angularVelocity.magnitude;

            if (PlayerScript._deviceDominant.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && firstPickedUp && fireRateTimer == fireRate && this.gameObject.CompareTag("Collected"))
            {
                //Instantiate(_gunShotTrailPrefab, shootPoint.transform);

                Fire();

                fireRateTimer = 0f;
            }
        }

        if (_collectable.isCollected && !firstPickedUp)
        {
            firstPickedUp = true;
        }

        if (fireRateTimer < fireRate)
        {
            fireRateTimer += Time.deltaTime;
        } else
        {
            fireRateTimer = fireRate;
            if (!clickSoundPlayed)
            {
                PlaySound.PlayAudio(_audioSource[2], true, -.15f, .15f);
                clickSoundPlayed = true;
            }
        }

        //Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.green, .1f);
        //Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.green);

        if (_collectable.isCollected)
            gunBody.transform.localRotation = Quaternion.Lerp(gunBody.transform.localRotation, gunBodyBaseRotation, Time.deltaTime * gunRecoilAngleSpeed);

        if (_previousTag != this.gameObject.tag)
        {
            CheckLights();
            _previousTag = this.gameObject.tag;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destructible" && this.gameObject.CompareTag("Collected"))
        {
            if (deviceAngularVelocity > 2f)
            {
                Destructible destructible = other.transform.GetComponent<Destructible>();

                destructible.HitPiece();
            }
        }
    }

    public void Fire()
    {
        //print("Fired");
        //shotTrail.Play();

        muzzleFlash.Play();



        RaycastHit hit;
        Ray ray = new Ray(shootPoint.transform.position, shootPoint.transform.forward);

        // Cast a ray that ignores triggers so we don't hit player trigger objects
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            AddExplosionForce(hit.point, explosionRadius, explosionForce);


            //if (!hit.collider.CompareTag("Enemy"))
            //    SpawnDecal(hit);

            var hitPoint = hit.point;

            if (hit.collider.CompareTag("Destructible"))
            {
                Destructible destructible = hit.transform.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.DestroyIntoPieces();
                }

                //AddExplosionForce(hit.point, explosionRadius, explosionForce);
            }

            //print("Object Hit:" + hit.collider.gameObject.name);
            //print("Object Hit:" + hit.collider.gameObject.tag);


            if (hit.collider.CompareTag("Enemy"))
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

                //AddExplosionForce(hit.point, explosionRadius, explosionForce);



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

            //_audioSource[0].PlayOneShot(clips[0]);
            //_audioSource[1].PlayOneShot(clips[1]);

            PlaySound.PlayAudio(_audioSource[0], true, -.15f, .15f);
            PlaySound.PlayAudio(_audioSource[1], false, 0, 0);
            clickSoundPlayed = false;

            HapticFeedback.HapticAmount(1000, true, false);

            //globalLowPass.GunShotLowPass(3000);

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

    private void CheckLights()
    {
        if (this.gameObject.CompareTag("Collected"))
        {
            foreach (var light in _lights)
            {
                light.enabled = true;
            }
        }
        else
        {
            foreach (var light in _lights)
            {
                light.enabled = false;
            }
        }
    }

    private void SpawnDecal(RaycastHit hitInfo)
    {
        var decal = DecalPool._instance.GetFromPool();
        decal.transform.parent = null;
        decal.transform.position = hitInfo.point;
        decal.transform.forward = hitInfo.normal * -1f;
    }
}
