using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoseController : MonoBehaviour
{   public string CurrentScene;
    private void OnTriggerEnter2D(Collider2D BoundaryCollision) 
    {
        if(BoundaryCollision.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("Player fell off!!");
            SceneManager.LoadScene(CurrentScene);
        }     
    }
}
