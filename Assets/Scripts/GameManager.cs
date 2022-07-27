
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager 
{
    // public static GameManager Instance { get; private set; }
    
    public static IEnumerator ReloadCurrentScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
