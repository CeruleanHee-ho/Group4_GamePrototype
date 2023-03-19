using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text scoreText;
    public PlayerController playerCon;
    public ScoreSystem scoreSys;
    public bool gameOver = false;
    private float scoreStartX = -105f;
    private float scoreStartY = 90f;
    private float scoreGameOverX = 0f;
    private float scoreGameOverY = 0f;

    // Scale factor is set to 10, which messes with the positioning.
    void Start()
    {
        scoreStartX = (scoreStartX * 10) + 1920;
        scoreStartY = (scoreStartY * 10) + 1080;
        scoreGameOverX = (scoreGameOverX * 10) + 1920;
        scoreGameOverY = (scoreGameOverY * 10) + 1080;
    }
    
    void Update()
    {
        if (!playerCon)
        {
            gameOver = true;
        }
        else
        {
            gameOver = false;
        }
        
        // For normal score text on the top-left of the screen.
        scoreText.text = "Score: " + scoreSys.score.ToString(); // Displays score.
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
        }
    }
}
