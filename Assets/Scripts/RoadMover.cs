using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RoadSpawner))]
public class RoadMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _increaseSpeedSize;
    [SerializeField] private float _thresholdOne = 15;
    [SerializeField] private float _thresholdTwo = 25;
    

    private List<RoadExtender> _roads;

    public List<RoadExtender> Roads => _roads;

    public int Phase
    {
        get
        {
            if (_speed > _thresholdTwo) return 3;
            if (_speed > _thresholdOne) return 2;
            return 1;
        }
    }

    private void Start()
    {
        _roads = FindObjectsOfType<RoadExtender>().ToList();
        foreach (var road in _roads)
        {
            road.RoadSpawner = GetComponent<RoadSpawner>();
        }
    }

    private void Update()
    {
        for (int i = 0; i < Roads.Count; i++)
        {
            Roads[i].transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }
        IncreaseSpeed();
    }

    private void IncreaseSpeed() => _speed += Time.deltaTime * _increaseSpeedSize;
}
