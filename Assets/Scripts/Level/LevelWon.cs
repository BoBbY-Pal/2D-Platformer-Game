using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWon : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null) return;
        Debug.Log("Level Completed!!");
        LevelsManager.Instance.MarkLevelComplete();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
