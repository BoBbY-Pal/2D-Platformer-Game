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
    {
        SceneManager.LoadScene(LevelName);
    }
}
