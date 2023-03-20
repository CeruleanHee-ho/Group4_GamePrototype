using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public PlayerController player;
    
    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (player.gameOver && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("R is pressed");
            ResetTheGame();
        }
    }
}
