using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    private static bool _isQuitting;

    [SerializeField]
    private AssetReference _firstRoom;

    
    public static GameManager Instance
    {
        get
        {
            if (_isQuitting)
                return null;

            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    var go = new GameObject("GameManager[SINGLETON]");
                    _instance = go.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

#endregion
    public AssetReference LastLoadedRoom;

    public Action<AssetReference> RoomLoadedCallback;

    public Transform PlayerCamera;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        if (!PlayerCamera)
            PlayerCamera = Camera.main.transform;

        LoadFirstRoom();

    }

    private void LoadFirstRoom()
    {
        Addressables.LoadSceneAsync(_firstRoom, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void OnLoadedRoom(AssetReference sceneRef)
    {
        LastLoadedRoom = sceneRef;
        RoomLoadedCallback?.Invoke(sceneRef);
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
