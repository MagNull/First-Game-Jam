using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RoadMover))]
public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private RoadExtender[] _easyRoads;
    [SerializeField] private RoadExtender[] _mediumRoads;
    [SerializeField] private RoadExtender[] _hardRoads;
    
    [SerializeField] private float _probabilityToEasyRoad;
    [SerializeField] private float _probabilityToMediumRoad;
    [SerializeField] private float _probabilityToHardRoad;
    private RoadMover _roadMover;

    private void Awake()
    {
        _roadMover = GetComponent<RoadMover>();
    }
    
    public RoadExtender SpawnRoad(Vector3 position)
    {
        RoadExtender newRoad = Instantiate(GetRandomRoad(), position, Quaternion.identity);
        newRoad.RoadSpawner = this;
        newRoad.GetComponent<RoadUtilizer>().RoadSpawner = this;
        _roadMover.Roads.Add(newRoad);
        return newRoad;
    }

    private RoadExtender GetRandomRoad()
    {
        float roll = Random.Range(1, 101);
        if (roll > _probabilityToEasyRoad)
        {
            roll = Random.Range(1, _probabilityToMediumRoad + _probabilityToHardRoad + 1);
            if (roll <= _probabilityToHardRoad)
            {
                return _hardRoads[Random.Range(0, _hardRoads.Length)];
            }
            return _mediumRoads[Random.Range(0, _mediumRoads.Length)];
        }
        return _easyRoads[Random.Range(0, _easyRoads.Length)];
    }

    private void Update()
    {
        ChangeProbabilities();
    }

    private void ChangeProbabilities()
    {
        switch (_roadMover.Phase)
        {
            case 1:
                _probabilityToEasyRoad = 80;
                _probabilityToMediumRoad = 20;
                _probabilityToHardRoad = 0;
                break;
            case 2:
                _probabilityToEasyRoad = 40;
                _probabilityToMediumRoad = 55;
                _probabilityToHardRoad = 5;
                break;
            case 3:
                _probabilityToEasyRoad = 30;
                _probabilityToMediumRoad = 50;
                _probabilityToHardRoad = 20;
                break;
        }
    }

    public void DestroyRoad(RoadExtender road)
    {
        _roadMover.Roads.Remove(road);
        Destroy(road.gameObject);
    }
}