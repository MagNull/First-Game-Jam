using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LinkingMovement : MonoBehaviour, ILinkable
{
    [SerializeField] private Collider _trigger;
    private Rigidbody _rigidbody;
    private Rigidbody _linkedRigidbody;
    private Transform _parent;
    
    public bool IsLinked { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _parent = transform.parent;
    }

    public void Link(Rigidbody linkRigidbody)
    {
        _linkedRigidbody = linkRigidbody;
        transform.parent = null;
        IsLinked = true;
        _trigger.enabled = false;
    }

    public void Unlink()
    {
        _linkedRigidbody.GetComponent<ILinkable>().Unlink();
        _rigidbody.velocity = Vector3.zero;
        _linkedRigidbody = null;
        transform.parent = _parent;
        IsLinked = false;
        _trigger.enabled = true;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (IsLinked)
        {
            _rigidbody.velocity = _linkedRigidbody.velocity;
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
