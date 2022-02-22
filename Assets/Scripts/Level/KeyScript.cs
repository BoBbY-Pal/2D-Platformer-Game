using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyScript : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D KeyCollision) 
    {
        if(KeyCollision.gameObject.GetComponent<PlayerController>() != null) {
            PlayerController playerController = KeyCollision.gameObject.GetComponent<PlayerController>();
            playerController.PickUpKey();
            Destroy(gameObject);
        }     
    }
}
