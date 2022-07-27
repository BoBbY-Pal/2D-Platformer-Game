using UnityEngine;


public class Key : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D keyCollision)
    {
        if (keyCollision.gameObject.GetComponent<PlayerController>() == null) return;
        PlayerController playerController = keyCollision.gameObject.GetComponent<PlayerController>();
        playerController.KeyPickUp();
        Destroy(gameObject);
    }
}
