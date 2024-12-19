using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene (index 1)
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

    public void ResumeGame()
    {
        // Set time scale back to normal to resume gameplay
        Time.timeScale = 1;

        // Unload the pause menu scene (index 3)
        SceneManager.UnloadSceneAsync(3);
    }
}
