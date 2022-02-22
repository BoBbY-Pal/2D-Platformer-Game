using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{   
    public Button playButton;
    void Awake() 
    {
        playButton.onClick.AddListener(LevelSelection);  
    }

    private void LevelSelection()
    {  
        SoundManager.Instance.Play(Sounds.StartButtonClick);
        SceneManager.LoadScene(1);
    }
}
