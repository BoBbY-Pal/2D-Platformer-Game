using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour 
{
    private Button _button;
    public string levelName;
    void Awake() 
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {   LevelStatus levelStatus = LevelsManager.Instance.GetLevelStatus(levelName);
       switch (levelStatus) {
            
            case LevelStatus.Locked:
               SoundManager.Instance.Play(Sounds.ButtonClick);
               Debug.Log("This level is locked!!");
               break;
            case LevelStatus.Unlocked:
               SoundManager.Instance.Play(Sounds.ButtonClick);
               SceneManager.LoadScene(levelName);
               break;
            case LevelStatus.Completed:
               SoundManager.Instance.Play(Sounds.ButtonClick);
               SceneManager.LoadScene(levelName);
               break;
       }
    }
}
