using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedSwapper : MonoBehaviour
{
    private Transform _linkedTransform;

    public Transform LinkedTransform
    {
        set => _linkedTransform = value;
    }

    public bool IsLinking { private get; set; }

    private void Update()
    {
        Swap();
    }

    private void Swap()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _linkedTransform && !IsLinking)
        {
            Vector3 _linkedPosition = _linkedTransform.position;
            _linkedTransform.position = transform.position;
            transform.position = _linkedPosition;
        }
    }
}
