using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{   PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D EnemyCollision) 
    {   playerController = EnemyCollision.gameObject.GetComponent<PlayerController>();
        if(playerController != null) {
           StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()     
    {  
        playerController.KillPlayer();
        yield return new WaitForSeconds(4);
        // playerController.ReloadCurrentScene();
    }
    
} 
