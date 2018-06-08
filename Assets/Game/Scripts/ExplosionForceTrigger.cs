using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForceTrigger : MonoBehaviour
{

    public ExplosionForce explosionForcePoint;

    public bool disableColliderOnTrigger;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            explosionForcePoint.DoExplosionForce();

            if (disableColliderOnTrigger)
                this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
