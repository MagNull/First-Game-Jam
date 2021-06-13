using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _countSpeed = 1;
    private float _points;

    public float Points => _points;

    private void Update()
    {
        _points = Points + Time.deltaTime * _countSpeed;
        _text.text = ((int)Points).ToString();
    }
}
