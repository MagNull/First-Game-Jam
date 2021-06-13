using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadUtilizer))]
public class RoadExtender : MonoBehaviour
{
    [HideInInspector] public RoadSpawner RoadSpawner;

    [HideInInspector] public bool Extended = false;

    [HideInInspector] public RoadExtender RightNeighbour;
    [HideInInspector] public RoadExtender LeftNeighbour;
    
    [HideInInspector] public RoadExtender ExtensionRoadExtender;
    public RoadExtender PreviousRoad;

    [SerializeField] private float _roadDistance = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            CreateNeighbourRoads();

            ExtendRoads();
            
            CheckExtendedNeighbour(this);
            CheckExtendedNeighbour(RightNeighbour);
            CheckExtendedNeighbour(LeftNeighbour);
        }
    }

    private void ExtendRoads()
    {
        if (!Extended)
        {
            ExtendRoad(this);
        }

        if (!RightNeighbour.Extended)
        {
            ExtendRoad(RightNeighbour);
        }

        if (!LeftNeighbour.Extended)
        {
            ExtendRoad(LeftNeighbour);
        }
    }

    private void CreateNeighbourRoads()
    {
        if (RightNeighbour is null)
        {
            RightNeighbour = RoadSpawner.SpawnRoad(transform.position + Vector3.right * _roadDistance);
            RightNeighbour.LeftNeighbour = this;
        }

        if (LeftNeighbour is null)
        {
            LeftNeighbour = RoadSpawner.SpawnRoad(transform.position + Vector3.left * _roadDistance);
            LeftNeighbour.RightNeighbour = this;
        }
    }

    private void ExtendRoad(RoadExtender roadExtender)
    {
        roadExtender.ExtensionRoadExtender = RoadSpawner.SpawnRoad(roadExtender.transform.position +
                                                   Vector3.forward *
                                                   roadExtender.transform.localScale.z);
        roadExtender.ExtensionRoadExtender.PreviousRoad = roadExtender;
        roadExtender.ExtensionRoadExtender.GetComponent<RoadUtilizer>().RoadNumber =
            roadExtender.GetComponent<RoadUtilizer>().RoadNumber + 1;
        roadExtender.Extended = true;
    }

    private void CheckExtendedNeighbour(RoadExtender roadExtender)
    {
        if (roadExtender.RightNeighbour &&
            roadExtender.RightNeighbour.Extended) roadExtender.ExtensionRoadExtender.RightNeighbour = roadExtender.RightNeighbour.ExtensionRoadExtender;
        if (roadExtender.LeftNeighbour &&
            roadExtender.LeftNeighbour.Extended) roadExtender.ExtensionRoadExtender.LeftNeighbour = roadExtender.LeftNeighbour.ExtensionRoadExtender;
    }
}
