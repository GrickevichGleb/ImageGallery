using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject persistentObjectsPrefab = null;

    private static bool _isSpawned = false;
    
    private void Awake()
    {
        if (_isSpawned) return;
        
        SpawnPersistentObjects();
        _isSpawned = true;
    }


    private void SpawnPersistentObjects()
    {
        GameObject persistentObject = Instantiate(persistentObjectsPrefab);
        DontDestroyOnLoad(persistentObject);
    }
}
