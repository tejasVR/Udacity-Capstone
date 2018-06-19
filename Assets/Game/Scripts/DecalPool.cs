using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalPool : MonoBehaviour {

    [SerializeField]
    private GameObject _decalPrefab;

    private Queue<GameObject> _availablePrefabs = new Queue<GameObject>();

    public static DecalPool _instance { get; private set; }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        GrowPool();
    }	

    public GameObject GetFromPool()
    {
        if (_availablePrefabs.Count == 0)
            GrowPool();

        var instance = _availablePrefabs.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    private void GrowPool()
    {
        for(int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(_decalPrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    private void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        _availablePrefabs.Enqueue(instance);
    }
}
