using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjCounter : MonoBehaviour {

    public float counterToDisableObj;

	// Use this for initialization
	void Start () {
        StartCoroutine(DisableObjCount());
	}


    IEnumerator DisableObjCount()
    {
        yield return new WaitForSeconds(counterToDisableObj);
        this.gameObject.SetActive(false);
    }
}
