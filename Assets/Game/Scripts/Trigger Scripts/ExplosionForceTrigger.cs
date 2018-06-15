using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForceTrigger : MonoBehaviour
{

    public ExplosionForce[] _explosionForcePoint;

    public bool _disableColliderOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var point in _explosionForcePoint)
            {
                point.DoExplosionForce();
            }

            if (_disableColliderOnTrigger)
                this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
