using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Colorsforkeys : MonoBehaviour
{
    [Header("Text Components")]
    public TextMeshProUGUI titleText; // Title with controls
    public TextMeshProUGUI subtitleText1;
    public TextMeshProUGUI subtitleText2;
    public TextMeshProUGUI subtitleText3;

    [Header("Color Settings")]
    public float titleColorChangeInterval = 1f;
    public float subtitleColorChangeInterval = 2f;
    private List<Color> titleColors;
    private List<Color> subtitleColors;
    private int currentTitleColorIndex = 0;
    private int currentSubtitleColorIndex = 0;

    private void Start()
    {
        // Initialize title and subtitle colors with space-themed colors
        titleColors = new List<Color>
        {
            new Color(1f, 0.8f, 0.8f), // Light Red
            new Color(0.8f, 0.4f, 0.4f), // Dark Red
            new Color(1f, 1f, 0.8f), // Light Yellow
            new Color(0.8f, 0.8f, 0.4f), // Dark Yellow
            new Color(0.8f, 1f, 0.8f), // Light Green
            new Color(0.4f, 0.8f, 0.4f), // Dark Green
            new Color(0.8f, 1f, 1f), // Light Cyan
            new Color(0.4f, 0.8f, 0.8f), // Dark Cyan
            new Color(0.8f, 0.8f, 1f), // Light Blue
            new Color(0.4f, 0.4f, 0.8f), // Dark Blue
            new Color(1f, 0.8f, 1f), // Light Magenta
            new Color(0.8f, 0.4f, 0.8f), // Dark Magenta
            new Color(1f, 1f, 1f), // White
            new Color(0.5f, 0.5f, 0.5f), // Grey
            new Color(0.2f, 0.2f, 0.2f) // Dark Grey
        };

        subtitleColors = new List<Color>
        {
            new Color(1f, 0.8f, 0.8f), // Light Red
            new Color(0.8f, 0.4f, 0.4f), // Dark Red
            new Color(1f, 1f, 0.8f), // Light Yellow
            new Color(0.8f, 0.8f, 0.4f), // Dark Yellow
            new Color(0.8f, 1f, 0.8f), // Light Green
            new Color(0.4f, 0.8f, 0.4f), // Dark Green
            new Color(0.8f, 1f, 1f), // Light Cyan
            new Color(0.4f, 0.8f, 0.8f), // Dark Cyan
            new Color(0.8f, 0.8f, 1f), // Light Blue
            new Color(0.4f, 0.4f, 0.8f), // Dark Blue
            new Color(1f, 0.8f, 1f), // Light Magenta
            new Color(0.8f, 0.4f, 0.8f), // Dark Magenta
            new Color(1f, 1f, 1f), // White
            new Color(0.5f, 0.5f, 0.5f), // Grey
            new Color(0.2f, 0.2f, 0.2f) // Dark Grey
        };

        // Start the color change coroutines
        StartCoroutine(ChangeTitleColors());
        StartCoroutine(ChangeSubtitleColors());

        // Start the coroutine to load scene 2 after 5 seconds
        StartCoroutine(LoadSceneAfterDelay(5f));
    }

    private IEnumerator ChangeTitleColors()
    {
        while (true)
        {
            // Change the color of the title text
            titleText.color = titleColors[currentTitleColorIndex];

            // Update the color index
            currentTitleColorIndex = (currentTitleColorIndex + 1) % titleColors.Count;

            // Wait for the next color change
            yield return new WaitForSeconds(titleColorChangeInterval);
        }
    }

    private IEnumerator ChangeSubtitleColors()
    {
        while (true)
        {
            // Change the color of each subtitle text component
            Color newColor = subtitleColors[currentSubtitleColorIndex];
            subtitleText1.color = newColor;
            subtitleText2.color = newColor;
            subtitleText3.color = newColor;

            // Update the color index
            currentSubtitleColorIndex = (currentSubtitleColorIndex + 1) % subtitleColors.Count;

            // Wait for the next color change
            yield return new WaitForSeconds(subtitleColorChangeInterval);
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }
}