using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text scoreText;
    public Text bonusText;
    public Text gameOverText;
    public PlayerController playerCon;
    public ScoreSystem scoreSys;
    public bool gameOver = false;
                           //private float scoreStartX = 85f;
                           //private float scoreStartY = -20f;
                           //private float scoreGameOverX = 245f;
                           //private float scoreGameOverY = -105f;

    /* Scale factor is set to 10, which messes with the positioning.
    void Start()
    {
        scoreStartX = (scoreStartX * 10) + 1920;
        scoreStartY = (scoreStartY * 10) + 1080;
        scoreGameOverX = (scoreGameOverX * 10) + 1920;
        scoreGameOverY = (scoreGameOverY * 10) + 1080;
    }*/

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
        scoreText.text = "Score: " + scoreSys.score.ToString() + "\n" // " "\n" creates a line break (return key).
                       + "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString(); // Displays score and high score.

        if (scoreSys.aboveBonusGet && !scoreSys.laneBonusGet)
        {
            bonusText.color = new Color(25, 75, 0, 1);
            bonusText.text = "Jump Bonus!";
        }

        if (scoreSys.laneBonusGet)
        {
            bonusText.color = new Color(0, 75, 0, 1);
            bonusText.text = "Lane-Switch Bonus!";
        }

        if (!scoreSys.aboveBonusGet && !scoreSys.laneBonusGet)
        {
            bonusText.text = "";
        }

        if (gameOver)
        {
            // Game Over positions
            //scoreText.transform.position = new Vector2(scoreGameOverX, scoreGameOverY);
            //scoreText.alignment = TextAnchor.MiddleCenter;
            gameOverText.text = "Game Over!"; // Displays game over text.
        }
        else
        {
            // Starting positions.
            //scoreText.transform.position = new Vector2(scoreStartX, scoreStartY);
            scoreText.alignment = TextAnchor.UpperLeft;
            gameOverText.text = ""; // Game over text has no value.
        }
    }
}
