using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public Scene _sceneToLoad;
    public Scene _sceneToUnload;

    private bool _isLoading;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }

    IEnumerator SwitchScene()
    {
        _isLoading = true;
        yield return new WaitForSeconds(2f);
       
    }

}
