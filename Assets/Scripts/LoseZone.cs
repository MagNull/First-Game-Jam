using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseZone : MonoBehaviour
{
    private GameState _gameState;

    private void Awake()
    {
        _gameState = FindObjectOfType<GameState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement link) ||
            (other.gameObject.TryGetComponent(out LinkingMovement linking) && linking.IsLinked))
        {
            _gameState.Lose();
        }
    }
}
