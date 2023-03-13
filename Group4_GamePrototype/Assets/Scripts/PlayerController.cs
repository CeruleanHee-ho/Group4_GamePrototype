using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private int playerPosX = 0;
    private float playerPosY = 0.5f;
    private float playerPosZ;
    public float jumpForce = 10;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    float velocity;
    public bool isOnGround;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool gameOver = false;
    #endregion
    
    void Update()
    {
        playerPosZ = -3.5f;

        // Player starts in the middle, but the X position of the player can change in accordance to the 'A' and 'D' keys.




        #region Lane Movement
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

        if (transform.position.y <= 0.5f)
        {
            isOnGround = true;
        }

        if (isOnGround)
        {
            velocity = 0;
            transform.position = new Vector3(playerPosX, 0.5f, playerPosZ);
        }
        #endregion
    }
}
