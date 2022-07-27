using System;
using UnityEngine;


public class Idle : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D enemyCollision) 
    {  
        _playerController = enemyCollision.gameObject.GetComponent<PlayerController>();
        
        if(_playerController != null) {
            _playerController.PlayerDied();
        }
    }
} 
