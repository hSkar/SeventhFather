using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private Transform _headTransform;
    [SerializeField]
    private bool _lookAtPlayer;
    [SerializeField]
    private bool _rotateHeight;
    [Range(0, 1)]
    [SerializeField]
    private float _weight;
    private Quaternion _defaultRotation;
    private void Awake()
    {
        _defaultRotation = _headTransform.rotation;
        playerTransform = GameManager.Instance.PlayerCamera;

        if (_headTransform == null)
            _headTransform = transform;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (!_lookAtPlayer)
            return;

        if (playerTransform)
        {
            if (_rotateHeight)
            {
                _headTransform.rotation = Quaternion.Slerp(_defaultRotation, Quaternion.LookRotation(_headTransform.position - playerTransform.position), _weight);
                return;
            }

            Vector3 offset = _headTransform.position - playerTransform.position;
            offset.y = 0;
            offset = offset.normalized;

            if (_headTransform)
            {
                _headTransform.rotation = Quaternion.LookRotation(offset, Vector3.up);
            }
        }
    }
}
