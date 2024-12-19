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
            PlayerPrefs.Save();
        }

        // Update UI texts
        currentScoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }

    public void PlayGame()
    {
        // Reset current score before starting new game
        PlayerPrefs.SetInt("CurrentScore", 0);
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(1); // Load main game scene
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}