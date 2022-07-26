using UnityEngine;


public class EnemyController : MonoBehaviour
{
    private PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D enemyCollision) 
    {  
        _playerController = enemyCollision.gameObject.GetComponent<PlayerController>();
        if(_playerController != null) {
            _playerController.PlayerDied();
        }
    }
} 
