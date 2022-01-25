using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour 
{
    public Button restartButton;
   
    private void Awake() {
        restartButton.onClick.AddListener(RestartGame);
    }

    public void PlayerDied() 
    {
        gameObject.SetActive(true);         //It Wil enable the game over UI
    }

    private void RestartGame()
    {
        Debug.Log("Restart Button ClICKED");
        SceneManager.LoadScene(0);
    }
}