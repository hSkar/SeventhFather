using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneTrigger : InteractionTrigger
{
    [SerializeField]
    private AssetReference _sceneToLoad;

    private bool _sceneLoaded;    

    protected override void TriggerEnter(Collider coll)
    {
        if (!_sceneLoaded)
        {
            Addressables.LoadSceneAsync(_sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Additive, true).Completed += OnSceneLoaded;
        }
    }

    public void ManualLoad()
    {
        TriggerEnter(null);
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> sceneHandle)
    {
        //sceneHandle.Result.Activate();
        _sceneLoaded = true;

        sceneHandle.Result.Scene.GetRootGameObjects()[0].GetComponent<SceneRoot>().SceneInstance = sceneHandle.Result;

        GameManager.Instance.OnLoadedRoom(_sceneToLoad);
    }
}
