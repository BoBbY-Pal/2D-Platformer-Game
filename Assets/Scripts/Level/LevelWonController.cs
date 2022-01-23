using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWonController : MonoBehaviour
{   public string NextScene;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("Level Completed!!");
            SceneManager.LoadScene(NextScene);
            // SceneManager.LoadScene(SceneManager.GetActiveScene() + 1);
        }     
    }
}