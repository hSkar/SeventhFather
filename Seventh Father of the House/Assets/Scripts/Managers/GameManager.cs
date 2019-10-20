using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                var go = new GameObject("GameManager[SINGLETON]");
                _instance = go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if(_instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this.gameObject);
    }
#endregion
    public AssetReference LastLoadedRoom;

    public Action<AssetReference> RoomLoadedCallback;

    private Transform PlayerCamera;

    public void OnLoadedRoom(AssetReference sceneRef)
    {
        LastLoadedRoom = sceneRef;
        RoomLoadedCallback?.Invoke(sceneRef);
    }
}
