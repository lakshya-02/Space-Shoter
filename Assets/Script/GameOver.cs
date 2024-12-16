using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text currentScoreText;
    public Text highScoreText;

    private void Start()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); // Load main game scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
