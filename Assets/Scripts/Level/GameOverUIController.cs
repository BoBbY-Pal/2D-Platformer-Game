using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour 
{   
    public Button restartButton;
    public Button exitButton;
    private void Awake() {
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ReturnToMain);
    }

    public void PlayerDied() 
    {
        gameObject.SetActive(true);         //It Wil enable the game over UI
    }

    private void ReturnToMain()
    {   
        SoundManager.Instance.Play(Sounds.ExitButtonClick);
        SceneManager.LoadScene(0);
    }
    private void RestartGame()
    {   
        Debug.Log("Restart Button ClICKED");
        SoundManager.Instance.Play(Sounds.RestartButtonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}