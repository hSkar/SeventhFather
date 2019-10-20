using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private Transform _headTransform;

    private void Awake()
    {
        playerTransform = Camera.main.transform;

        if (_headTransform == null)
            _headTransform = transform;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (playerTransform)
        {
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
