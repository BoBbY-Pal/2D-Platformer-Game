using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoseController : MonoBehaviour
{   PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D BoundaryCollision) 
    {
        if(BoundaryCollision.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("Player fell off!!");
            playerController.ReloadScene();
        }     
    }
   
}
