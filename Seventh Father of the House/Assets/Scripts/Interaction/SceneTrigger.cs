using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneTrigger : InteractionTrigger
{
    [SerializeField]
    private AssetReference _sceneReference;

    private bool _sceneLoaded;
    private AsyncOperationHandle<SceneInstance> _sceneHandle;    

    protected override void TriggerEnter(Collider coll)
    {
        if (!_sceneLoaded)
        {
            _sceneReference.LoadSceneAsync().Completed += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> sceneHandle)
    {
        _sceneHandle = sceneHandle;
        
    }
}
