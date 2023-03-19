using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScorePT : MonoBehaviour
{
    public Text scoreText;
    public ScoreSystem scoreSys;
    private float scoreStartX = -105f;
    private float scoreStartY = 90f;

    // Scale factor is set to 10, which messes with the positioning.
    void Start()
    {
        scoreStartX = (scoreStartX * 10) + 1920;
        scoreStartY = (scoreStartY * 10) + 1080;
    }

    void Update()
    {

        // For normal score text on the top-left of the screen.
        scoreText.text = "Score: " + scoreSys.score.ToString(); // Displays score.
        /*
        if (gameOver)
        {
            // Game Over positions
            scoreText.transform.position = new Vector2(scoreGameOverX, scoreGameOverY);
            scoreText.alignment = TextAnchor.MiddleCenter;
        }
        else
        {
            // Starting positions.
            scoreText.transform.position = new Vector2(scoreStartX, scoreStartY);
            scoreText.alignment = TextAnchor.UpperLeft;
        } */
    }
}
