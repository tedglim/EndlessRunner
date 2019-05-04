using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    private int score;
    public Text scoreText;
    public Text bestText;
    public Text yourText;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void GameOver()
    {
        Invoke("ShowGameOver", 1.0f);
    }

    void ShowGameOver() {
        scoreText.gameObject.SetActive(false);
        if(score > PlayerPrefs.GetInt("Best", 0))
        {
            PlayerPrefs.SetInt("Best", score);
        }
        bestText.text = "Best Score: " + PlayerPrefs.GetInt("Best", 0).ToString();
        yourText.text = "Your Score: " + score.ToString();
        gameOverPanel.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadSceneAsync(0);
    }
}
