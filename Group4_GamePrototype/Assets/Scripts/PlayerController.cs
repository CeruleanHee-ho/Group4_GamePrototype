using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    // Positioning stuffs.
    private int playerPosX = 0; // X value for player (is applied).
    private float playerPosY = 0.5f; // Y value for player (is applied).
    private float playerPosZ; // Z value for player (is applied).

    // Jumping physics.
    public float jumpForce = 10;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    float velocity;
    
    public bool isOnGround;
    public float distance; // Length of the player's hitbox.
    public bool gameOver = false;

    public GameObject selfObject;
    #endregion

    void Update()
    {
        playerPosZ = -3.5f; // Keeps the player locked into the same Z position.

        #region Lane Movement
        // Player starts in the middle, but the X position of the player can change in accordance to the 'A' and 'D' keys.
        if (Input.GetKeyDown(KeyCode.A) && isOnGround && transform.position.x > -2)
        {
            // Moves player to the left.
            playerPosX -= 2;
        }
        
        if (Input.GetKeyDown(KeyCode.D) && isOnGround && transform.position.x < 2)
            {
                // Moves player to the right.
                playerPosX += 2;
            }
        #endregion

        #region Jump
        velocity += gravity * gravityScale * Time.deltaTime;
        playerPosY = velocity;

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
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
        }
        #endregion

        #region Game Over
        if (gameOver)
        {
            Destroy(selfObject);
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Collision Detection
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

        //Method to draw the ray in scene for debug purpose
        //Debug.DrawRay(transform.position, direction * distance, Color.green);
        //Debug.DrawRay(transform.position, -direction * distance, Color.green);
        #endregion
    }
}
