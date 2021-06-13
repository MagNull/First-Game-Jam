using System;
using UnityEngine;

[RequireComponent(typeof(RoadExtender))]
public class RoadUtilizer : MonoBehaviour
{
    public int RoadNumber = 1;
    public RoadSpawner RoadSpawner;
    [SerializeField] private int _numberToDelete = 3;

    private void Start()
    {
        if (RoadNumber >= _numberToDelete && RoadNumber > 1)
        {
            RoadExtender road = GetComponent<RoadExtender>();
            while (road.PreviousRoad != null)
            {
                road = road.PreviousRoad;
            }
            RoadSpawner.DestroyRoad(road);
        }
    }
}