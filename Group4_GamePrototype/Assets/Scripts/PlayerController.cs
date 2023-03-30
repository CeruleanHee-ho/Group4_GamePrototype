using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    // Positioning stuffs.
    public int playerPosX = 0; // X value for player (is applied).
    private float playerPosY = 0.5f; // Y value for player (is applied).
    public float playerPosZ; // Z value for player (is applied).

    // Jumping physics.
    public float jumpForce = 10;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    public float velocity;
    
    public bool isOnGround;
    public bool aboveEnemy;
    public float distance; // Length of the player's hitbox.
    public bool gameOver = false;

    public GameObject selfObject;
    public SwipeDetection swipeScript;

    public Vector3 highestPos;
    public int score;
    #endregion

    void Update()
    {
        playerPosZ = -3.5f; // Keeps the player locked into the same Z position.

        #region Jump
        velocity += gravity * gravityScale * Time.deltaTime;
        playerPosY = velocity;

        if (swipeScript.isSwiping && isOnGround)
        {
            velocity = jumpForce;
            isOnGround = false;
        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        // If the player's Y position reaches where the floor is positioned, then the player will maintain that Y position (makeshift ground detection).
        if (transform.position.y <= 0.5f && !gameOver)
        {
            isOnGround = true;
        }

        if (isOnGround)
        {
            velocity = 0;
            transform.position = new Vector3(playerPosX, 0.5f, playerPosZ);
            highestPos = transform.position;
            swipeScript.isSwiping = false;
        }

        // Captures the player's highest position while jumping. The position resets when the player is grounded again.
        if (!isOnGround && transform.position.y > highestPos.y)
        {
            highestPos = transform.position;
        }
        #endregion

        #region Game Over
        if (gameOver)
        {
            Destroy(selfObject);
        }
        #endregion
    }

    #region Collision Detection
    void FixedUpdate()
    {
        // There are rays on both sides of the player on the Z axis, and if either of those rays touch an object with the "Enemy" layer, then it's Game Over.
        Vector3 position = transform.position;
        Vector3 direction = Vector3.forward;
        if (Physics.Raycast(position, direction, distance, LayerMask.GetMask("Enemy")))
        {
            gameOver = true;
            //Debug.Log("Ya' dead!");
        }

        if (Physics.Raycast(position, -direction, distance, LayerMask.GetMask("Enemy")))
        {
            gameOver = true;
            //Debug.Log("Ya' dead!");
        }

        // This raycast will check if there is an enemy cube below the player while they are jumping (the player only gets extra points if they are specifically jumping over an enemy cube).
        if (Physics.Raycast(position, -Vector3.up, (distance * 5), LayerMask.GetMask("Enemy")))
        {
            aboveEnemy = true;
            //Debug.Log("Bonus Points!!!");
        }
        else
        {
            aboveEnemy = false;
        }

        // This raycast will check if the player is touching an enemy cube from below (effective when the player jumps up while an enemy cube is above them).
        if (Physics.Raycast(position, Vector3.up, (distance * 6), LayerMask.GetMask("Enemy")))
        {
            gameOver = true;
        }

        //Method to draw the ray in scene for debug purpose
        Debug.DrawRay(transform.position, direction * distance, Color.green); // In front of player.
        Debug.DrawRay(transform.position, -direction * distance, Color.green); // Behind player.
        Debug.DrawRay(transform.position, -Vector3.up * (distance * 5), Color.green); // Below player.
        Debug.DrawRay(transform.position, Vector3.up * (distance * 6), Color.green); // Above player.
    }
    #endregion

    #region Lane Movement
    // Player starts in the middle, but the X position of the player can change in accordance to the left and right buttons shown on the screen.
    public void MoveLeft()
    {
        Debug.Log("Left");
        if (isOnGround && transform.position.x > -2)
        {
            // Moves player to the left.
            playerPosX -= 2;
        }
    }

    public void MoveRight()
    {
        Debug.Log("Right");
        if (isOnGround && transform.position.x < 2)
        {
            // Moves player to the right.
            playerPosX += 2;
        }
    }
    #endregion
}
