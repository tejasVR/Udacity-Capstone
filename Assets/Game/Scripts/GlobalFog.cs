using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFog : MonoBehaviour {

    private float _fogEnd = 12;
    //RenderSettings settings = Object.FindObjectOfType<RenderSettings>();

    private void Start()
    {
        print(RenderSettings.fogEndDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _fogEnd = 15;
            GetComponentInChildren<BoxCollider>().enabled = false;
        }
        
    }

    private void Update()
    {
        if (Mathf.Abs(_fogEnd - RenderSettings.fogEndDistance) > .05f)
        {
            //print(RenderSettings.fogEndDistance);
            RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, _fogEnd, Time.deltaTime * 2f);
        }
    }
}
