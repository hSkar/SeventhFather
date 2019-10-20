using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneRoot : MonoBehaviour
{
    [SerializeField]
    private AssetReference _sceneReference;
    [SerializeField]
    private int _yRotation;
    [SerializeField]
    private Transform _roomTransform;



    private void OnEnable()
    {
        _roomTransform.Rotate(0, _yRotation, 0);
    }
}
