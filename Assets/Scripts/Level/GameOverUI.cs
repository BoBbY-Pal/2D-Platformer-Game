using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour 
{   
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Animator gameOverEllen;
    
    private void Awake() {
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ReturnToMain);
    }

    public IEnumerator GameOver() 
    {
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.Play(SoundTypes.MusicDeathSting);
        gameObject.SetActive(true);   // Game over panel
        SoundManager.Instance.Mute(true);
    }

    private void ReturnToMain()
    {   
        SoundManager.Instance.Mute(false);
        SoundManager.Instance.Play(SoundTypes.BackButtonClick);
        SceneManager.LoadScene(0);
    }
    private void RestartGame()
    {   
        SoundManager.Instance.Mute(false);
        Debug.Log("Restart Button Clicked");
        SoundManager.Instance.Play(SoundTypes.RestartButtonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}