using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    public SteamVR_Action_Vector2 _movementVector;
    public SteamVR_Input_Sources _source;
    public Player _player;

    public Vector2 _movement;
    

    private void Update()
    {
        var hmd = _player.hmdTransform;
        var lookDirection = hmd.forward;

        lookDirection.y = 0;
        lookDirection = lookDirection.normalized;
        
        _movement = _movementVector[_source].axis;

        var localMovementDir = hmd.InverseTransformDirection(_movement).normalized;
        bool movingBackwards = false;
            //Vector3.Dot(localMovementDir, lookDirection) < 0;

        transform.position += (movingBackwards ? -1 : 1) * lookDirection * _movement.magnitude * _speed * Time.deltaTime;
    }
}
