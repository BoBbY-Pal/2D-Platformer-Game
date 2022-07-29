
using UnityEngine;


public class PlayerFellOff : MonoBehaviour
{   
    PlayerController _playerController;
    private void OnTriggerEnter2D(Collider2D boundaryCollision)
    {
        _playerController = boundaryCollision.gameObject.GetComponent<PlayerController>();
        if (_playerController == null) return;
        Debug.Log("Player fell off!!");
        
        _playerController.Died();
    }
}
