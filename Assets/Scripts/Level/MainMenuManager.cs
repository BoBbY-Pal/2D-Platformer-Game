using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {  
        SoundManager.Instance.Play(Sounds.StartButtonClick);
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        throw new NotImplementedException();
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
