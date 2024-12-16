using UnityEngine;
using UnityEngine.UI;

public class SpaceTextEffect : MonoBehaviour
{
    public Text uiText; // Reference to the Text component
    public float speed = 1.0f; // Speed of the color change

    private Color[] spaceColors = {
        new Color(0.0f, 0.5f, 1.0f),   // Bright Blue
        new Color(0.5f, 0.0f, 1.0f),   // Bright Purple
        new Color(1.0f, 0.0f, 0.5f),   // Bright Pink
        new Color(0.0f, 1.0f, 0.5f),   // Bright Green
        new Color(1.0f, 0.5f, 0.0f),   // Bright Orange
        new Color(1.0f, 0.0f, 0.0f),   // Bright Red
        new Color(0.5f, 1.0f, 0.0f),   // Bright Lime
        new Color(1.0f, 1.0f, 0.0f),   // Bright Yellow
        new Color(0.0f, 1.0f, 1.0f),   // Bright Cyan
        new Color(1.0f, 0.75f, 0.79f), // Light Pink
        new Color(0.65f, 0.0f, 1.0f),  // Bright Violet
        new Color(1.0f, 0.4f, 0.0f)    // Bright Coral
    };
    private int currentColorIndex = 0;
    private float transitionProgress = 0.0f;

    void Update()
    {
        if (uiText != null)
        {
            // Calculate the color transition
            transitionProgress += Time.deltaTime * speed;
            if (transitionProgress >= 1.0f)
            {
                transitionProgress = 0.0f;
                currentColorIndex = (currentColorIndex + 1) % spaceColors.Length;
            }
            Color currentColor = Color.Lerp(
                spaceColors[currentColorIndex],
                spaceColors[(currentColorIndex + 1) % spaceColors.Length],
                transitionProgress
            );

            // Apply the color to the text
            uiText.color = currentColor;
        }
    }
}
