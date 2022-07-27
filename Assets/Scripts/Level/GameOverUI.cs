using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour 
{   
    public Button restartButton;
    public Button exitButton;
    private void Awake() {
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ReturnToMain);
    }

    public IEnumerator GameOver() 
    {
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(true);         // Game over panel
    }

    private void ReturnToMain()
    {   
        SoundManager.Instance.Play(SoundTypes.BackButtonClick);
        SceneManager.LoadScene(0);
    }
    private void RestartGame()
    {   
        Debug.Log("Restart Button Clicked");
        SoundManager.Instance.Play(SoundTypes.RestartButtonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}