using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour 
{   
    
    private TextMeshProUGUI _scoreText;
    private int _score = 0;
    
    private void Awake ()
    {
        _scoreText = GetComponent<TextMeshProUGUI> ();
    }
    
    private void Start() 
    {
        UpdateScore();
    }
    
    public void IncreaseScore(int increment)
    {
            _score += increment;
            UpdateScore();
    }
    
    private void UpdateScore()
    {
        _scoreText.text = "Score: " + _score;
    }
}