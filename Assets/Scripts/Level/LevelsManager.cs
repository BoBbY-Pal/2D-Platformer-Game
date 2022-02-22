using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class LevelsManager : MonoBehaviour
{
    private static LevelsManager instance;
    public static LevelsManager Instance{ get { return instance; } }
    public string level1;
    public string[] levels;
    private void Awake() 
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() 
    {
        if(GetLevelStatus(level1) == LevelStatus.Locked) {
            SetLevelStatus(level1, LevelStatus.Unlocked);
        }
    }

    public void MarkLevelComplete() 
    {
        Scene currentScene = SceneManager.GetActiveScene();

        //  Set level status to completed 
        SetLevelStatus(currentScene.name, LevelStatus.Completed);       

        //  Unlock the next level        
        int currentSceneIndex = Array.FindIndex(levels, level => level == currentScene.name); 
        int nextSceneIndex = currentSceneIndex + 1;
        if(currentSceneIndex < levels.Length) {
            SetLevelStatus(levels[nextSceneIndex], LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelStatus = (LevelStatus) PlayerPrefs.GetInt(level, 0);
        return levelStatus;
    }
    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
        Debug.Log("Setting " + level +" status " + levelStatus);
    }
    
}