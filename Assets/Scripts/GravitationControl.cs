using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravitationControl : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void Update()
    {
        if (!Physics.Raycast(transform.position, -transform.up, 1f))
        {
            _rigidbody.useGravity = true;   
        }
    }
}
