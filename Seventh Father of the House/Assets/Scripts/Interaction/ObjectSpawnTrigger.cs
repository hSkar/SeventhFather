using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectSpawnTrigger : InteractionTrigger
{
    [SerializeField]
    private AssetReference _assetReference;

    private bool _assetLoaded;
    private GameObject _object;
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
        if (!_assetLoaded)
        {
            return;
        }

    }

    protected override void TriggerExit(Collider coll)
    {
        if (!_assetLoaded)
        {
            return;
        }
            
    }
}
