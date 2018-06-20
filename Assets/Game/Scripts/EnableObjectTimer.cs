using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectTimer : MonoBehaviour {

    public float _timeToAppear;
    public float _timeToDisappear;

    public GameObject obj;

    private void Start()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(_timeToAppear);
        obj.SetActive(true);
        yield return new WaitForSeconds(_timeToDisappear);
        obj.SetActive(false);



    }

}
