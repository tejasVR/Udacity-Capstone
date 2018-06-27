using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingOcclusion : MonoBehaviour {

    [SerializeField]
    List<Light> _lights = new List<Light>();

    public float _occludeDist = 25;

    void Update()
    {
        foreach (Light light in _lights)
        {
            if ((PlayerScript._playerEye.transform.position - light.transform.position).sqrMagnitude > _occludeDist * _occludeDist)
            {
                light.enabled = false;
            }
        else{
                light.enabled = true;
            }
        }
    }
}
