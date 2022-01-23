using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{   PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D EnemyCollision) 
    {
        if(EnemyCollision.gameObject.GetComponent<PlayerController>() != null) {
        //    playerController = EnemyCollision.gameObject.GetComponent<PlayerController>();
           StartCoroutine(PlayerDeath());
            
        }
        IEnumerator PlayerDeath()     
        {   playerController.KillPlayer();
            yield return new WaitForSeconds(3);
            playerController.ReloadScene();
        }
    }
}
