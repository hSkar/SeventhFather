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

    private UnityEngine.ResourceManagement.ResourceProviders.SceneInstance _sceneInstance;
    public UnityEngine.ResourceManagement.ResourceProviders.SceneInstance SceneInstance { get { return _sceneInstance; }set { _sceneInstance = value; _sceneInstanceSet = true; } }

    private bool _sceneInstanceSet = false;
    [SerializeField]
    private bool _translateOnEnable = true;

    private void OnEnable()
    {
        if (_translateOnEnable)
        {
            //if (_roomTransform == null)
            //    _roomTransform = this.transform;

            //_roomTransform.Rotate(0, _yRotation, 0);
        }

        GameManager.Instance.RoomLoadedCallback += OnNewRoomLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.RoomLoadedCallback -= OnNewRoomLoaded;
    }

    private void OnNewRoomLoaded(AssetReference obj)
    {
        Debug.Log("OnNewRoomLoaded: LoadedRoom=" + obj.editorAsset.name + "MyReference=" + _unloadReference.editorAsset.name);

        if (obj.RuntimeKey.Equals(_unloadReference.RuntimeKey))
        {
            if (!_sceneInstanceSet)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(this.gameObject.scene);
                return;
            }

            if (SceneInstance.Scene != null)
                Addressables.UnloadSceneAsync(SceneInstance);
        }
    }
}
