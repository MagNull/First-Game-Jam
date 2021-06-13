using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1;
    [SerializeField] private float _movementAmplitude;
    private Vector3 _positionRelativeToParent;


    private void Start()
    {
        _positionRelativeToParent = transform.position - transform.parent.position;
    }

    private void Update()
    {
        Vector3 newPosition = transform.parent.position + _positionRelativeToParent +
                              Mathf.Sin(Time.timeSinceLevelLoad * _movementSpeed) * _movementAmplitude *
                              transform.right;
        transform.position = newPosition;
    }
}
