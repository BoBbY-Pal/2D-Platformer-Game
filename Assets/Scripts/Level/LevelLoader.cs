using System.Collections;
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
               SoundManager.Instance.Play(SoundTypes.LevelSelected);
               StartCoroutine(LevelLockedPopUp());
               break;
            
            case LevelStatus.Unlocked:
               SoundManager.Instance.Play(SoundTypes.LevelSelected);
               SceneManager.LoadScene(levelName);
               break;
            
            case LevelStatus.Completed:
               SoundManager.Instance.Play(SoundTypes.LevelSelected);
               SceneManager.LoadScene(levelName);
               break;
       }
    }

    private IEnumerator LevelLockedPopUp()
    {
       LevelsManager.Instance.levelLockedMsg.SetActive(true);
       yield return new WaitForSeconds(3f);
       LevelsManager.Instance.levelLockedMsg.SetActive(false);
    }
}
