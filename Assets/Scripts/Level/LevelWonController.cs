using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWonController : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("Level Completed!!");
            LevelsManager.Instance.MarkLevelComplete();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }     
    }
}
