using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneRoot : MonoBehaviour
{
    [SerializeField]
    private AssetReference _sceneReference;
    [SerializeField]
    private AssetReference _unloadReference;
    [SerializeField]
    private int _yRotation;
    [SerializeField]
    private Transform _roomTransform;

    public UnityEngine.ResourceManagement.ResourceProviders.SceneInstance sceneInstance;

    [SerializeField]
    private bool _translateOnEnable = true;

    private void OnEnable()
    {
        if (!_translateOnEnable)
        {
            if (_roomTransform == null)
                _roomTransform = this.transform;

            _roomTransform.Rotate(0, _yRotation, 0);
        }

        GameManager.Instance.RoomLoadedCallback += OnRoomLoaded;
    }

    private void OnRoomLoaded(AssetReference obj)
    {
        if (obj.RuntimeKey.Equals(_unloadReference.RuntimeKey))
        {
            if(sceneInstance.Scene != null)
                Addressables.UnloadSceneAsync(sceneInstance);
        }
    }
}
