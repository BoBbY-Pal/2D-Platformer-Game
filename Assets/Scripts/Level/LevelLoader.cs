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
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {   LevelStatus levelStatus = LevelsManager.Instance.GetLevelStatus(LevelName);
       switch (levelStatus) {
            
            case LevelStatus.Locked:
               Debug.Log("This level is locked!!");
               break;
            case LevelStatus.Unlocked:
               SceneManager.LoadScene(LevelName);
               break;
            case LevelStatus.Completed:
                SceneManager.LoadScene(LevelName);
               break;
       }
    }
}
