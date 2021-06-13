using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Rigidbody _rigidbody;
    private float _moveX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    { 
        _moveX = Input.GetAxisRaw("Horizontal") * _speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveX, 0);
    }
}