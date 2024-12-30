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
        UpdateScores();
    }

    private void UpdateScores()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update high score if current score is higher
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // Display scores
        currentScoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;

        PlayerPrefs.Save();
    }

    public void RestartGame()
    {
        // Assuming your main game scene is at index 1
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
        UpdateScores(); // Update the displayed scores after resetting
    }
}