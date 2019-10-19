using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnTrigger : InteractionTrigger
{
    [SerializeField]
    private AssetReference _assetReference;

    private GameObject _object;
    private bool _assetLoaded;

    private void Awake()
    {
        _assetReference.LoadAssetAsync<GameObject>().Completed += OnAssetLoaded;    
    }

    private void OnAssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
        _object = obj.Result;
        _assetLoaded = true;
    }

    protected override void TriggerEnter(Collider coll)
    {
        
    }

    protected override void TriggerExit(Collider coll)
    {
        
    }
}
