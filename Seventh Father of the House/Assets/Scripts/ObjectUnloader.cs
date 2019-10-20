using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectUnloader : MonoBehaviour
{
    [SerializeField]
    private AssetReference _unloadReference;


    private void OnEnable()
    {
        GameManager.Instance.RoomLoadedCallback += OnRoomLoaded;
    }

    private void OnRoomLoaded(AssetReference obj)
    {
        if (!_unloadReference.RuntimeKeyIsValid())
            return;

        if (obj.RuntimeKey.Equals(_unloadReference.RuntimeKey))
        {
            Destroy(this.gameObject);
        }
    }
}
