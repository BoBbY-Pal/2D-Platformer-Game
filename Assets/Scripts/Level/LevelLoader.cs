using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour 
{
    private Button button;
    public string LevelName;
    void Awake() 
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {   LevelStatus levelStatus = LevelsManager.Instance.GetLevelStatus(LevelName);
       switch (levelStatus) {
            
            case LevelStatus.Locked:
               SoundManager.Instance.Play(Sounds.ButtonClick);
               Debug.Log("This level is locked!!");
               break;
            case LevelStatus.Unlocked:
               SoundManager.Instance.Play(Sounds.ButtonClick);
               SceneManager.LoadScene(LevelName);
               break;
            case LevelStatus.Completed:
               SoundManager.Instance.Play(Sounds.ButtonClick);
               SceneManager.LoadScene(LevelName);
               break;
       }
    }
}
