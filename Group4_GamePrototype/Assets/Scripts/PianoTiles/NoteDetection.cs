using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDetection : MonoBehaviour
{
    #region Variables
    //
    public ScoreSystemPT score;

    // Responsible for tracking prefabs.
    public GameObject tileNote0;
    public GameObject tileNote1;
    public GameObject tileNote2;
    public GameObject tileNote3;
    public GameObject tileNote4;

    // Responsible for deciding on which prefab to destroy, if any, when the player hits a piano key.
    public bool canHit1;
    public bool canHit2;
    public bool canHit3;
    public bool canHit4;
    public int i; // Helps iterate through four variables mentioned above.

    // Responsible for assigning a state to note tiles. Plays into how many points the player gets when a tile is destroyed.
    public int prefState1;
    public int prefState2;
    public int prefState3;
    public int prefState4;
    #endregion

    void Update()
    {
        // Tracks up to five prefabs at a time.
        #region Prefab Tracking
        tileNote0 = GameObject.FindGameObjectWithTag("Tile"); // Finds first available instance of prefab.
        if (tileNote0) // Once first available intance of prefab is occupied...
        {
            if (!tileNote1) // If first note variable is not occupied, then give tileNote0's value to this one. Repeat for the rest of them.
            {
                tileNote0.tag = "Tile1";
                tileNote1 = GameObject.FindGameObjectWithTag("Tile1");
                tileNote0 = null; // Reset tileNote0's value afterward to keep the loop going.
                //tileNote1.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (!tileNote2)
            {
                tileNote0.tag = "Tile2";
                tileNote2 = GameObject.FindGameObjectWithTag("Tile2");
                tileNote0 = null;
                //tileNote2.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else if (!tileNote3)
            {
                tileNote0.tag = "Tile3";
                tileNote3 = GameObject.FindGameObjectWithTag("Tile3");
                tileNote0 = null;
                //tileNote3.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (!tileNote4)
            {
                tileNote0.tag = "Tile4";
                tileNote4 = GameObject.FindGameObjectWithTag("Tile4");
                tileNote0 = null;
                //tileNote4.GetComponent<SpriteRenderer>().color = Color.green;
            }
            /* Debug stuff
            else
            {
                tileNote0.GetComponent<SpriteRenderer>().color = Color.black;
            } */
        }
        #endregion
    }

    // Code that has to do with raycasting.
    #region Raycasting
    void FixedUpdate()
    {
        #region Raycast Variables
        Vector2 position1 = new Vector2(-10, 1f); // Area where player is too early and object is destroyed.
        Vector2 position2 = new Vector2(-10, -1.5f); // Area where player is perfect and object is destroyed.
        Vector2 position3 = new Vector2(-10, -4f); // Area where player is too late and object is destroyed.
        Vector2 position4 = new Vector2(-10, -7f); // Area where note is completely off-screen.
        Vector2 direction = Vector2.right;
        float distance = 20;
        RaycastHit2D hit1 = Physics2D.Raycast(position1, direction, distance);
        RaycastHit2D hit2 = Physics2D.Raycast(position2, direction, distance);
        RaycastHit2D hit3 = Physics2D.Raycast(position3, direction, distance);
        RaycastHit2D hit4 = Physics2D.Raycast(position4, direction, distance);
        #endregion

        // Will the note be destroyed when the player hits a piano key?
        #region Tile Hit States
        // Only the piano closest to the bottom of the screen is given hit priority. When they go passed the final raycast, that priority is given to the next note that's closest to the bottom of the screen.
        // Notes aren't destroyed if they haven't gone passed any of the raycasts yet.
        if (i == 0 && !canHit2 && !canHit3 && !canHit4)
        {
            canHit1 = true;
        }

        if (i == 1 && !canHit1 && !canHit3 && !canHit4)
        {
            canHit2 = true;
        }

        if (i == 2 && !canHit1 && !canHit2 && !canHit4)
        {
            canHit3 = true;
        }

        if (i == 3 && !canHit1 && !canHit2 && !canHit3)
        {
            canHit4 = true;
        }
        #endregion


        // What state is the tile in when the player hits a piano key (has to do with score)?
        #region Tile Score States
        // Will set corresponding prefab to "Early" (1) state.
        if (hit1.collider != null)
        {
            if (hit1.transform.tag == "Tile1")
            {
                prefState1 = 1;
            }
            if (hit1.transform.tag == "Tile2")
            {
                prefState2 = 1;
            }
            
            if (hit1.transform.tag == "Tile3")
            {
                prefState3 = 1;
            }
            
            if (hit1.transform.tag == "Tile4")
            {
                prefState4 = 1;
            }
        }

        // Will set corresponding prefab to "Perfect" (2) state.
        if (hit2.collider != null)
        {
            if (hit2.transform.tag == "Tile1")
            {
                prefState1 = 2;
            }
            
            if (hit2.transform.tag == "Tile2")
            {
                prefState2 = 2;
            }
            
            if (hit2.transform.tag == "Tile3")
            {
                prefState3 = 2;
            }
            
            if (hit2.transform.tag == "Tile4")
            {
                prefState4 = 2;
            }
        }

        // Will set corresponding prefab to "Late" (3) state.
        if (hit3.collider != null)
        {
            if (hit3.transform.tag == "Tile1")
            {
                prefState1 = 3;
            }

            if (hit3.transform.tag == "Tile2")
            {
                prefState2 = 3;
            }

            if (hit3.transform.tag == "Tile3")
            {
                prefState3 = 3;
            }

            if (hit3.transform.tag == "Tile4")
            {
                prefState4 = 3;
            }
        }

        // Will set corresponding prefab to "Miss" (0) state and make the next prefab able to be hit.
        if (hit4.collider != null)
        {
            if (hit4.transform.tag == "Tile1")
            {
                prefState1 = 0;
                canHit1 = false;
                i = 1;

            }
            
            if (hit4.transform.tag == "Tile2")
            {
                prefState2 = 0;
                canHit2 = false;
                i = 2;
            }
            
            if (hit4.transform.tag == "Tile3")
            {
                prefState3 = 0;
                canHit3 = false;
                i = 3;
            }
            
            if (hit4.transform.tag == "Tile4")
            {
                prefState4 = 0;
                canHit4 = false;
                i = 0;
            }
        }
        #endregion

        //Method to draw the ray in scene for debug purposes.
        #region Debug Rays
        Debug.DrawRay(position1, direction * distance, Color.red);
        Debug.DrawRay(position2, direction * distance, Color.blue);
        Debug.DrawRay(position3, direction * distance, Color.yellow);
        Debug.DrawRay(position4, direction * distance, Color.green);
        #endregion
    }
    #endregion

    // Destroys corresponding prefabs and sets their hit state to false when piano key is pressed.
    public void DestroyPrefabs()
    {
        if (canHit1 && prefState1 != 0)
        {
            Destroy(tileNote1);
            score.amountToAdd = ScoreOut1(prefState1);
            prefState1 = 0;
            canHit1 = false;
            i = 1;
        }

        if (canHit2 && prefState2 != 0)
        {
            Destroy(tileNote2);
            score.amountToAdd = ScoreOut2(prefState2);
            prefState2 = 0;
            canHit2 = false;
            i = 2;
        }

        if (canHit3 && prefState3 != 0)
        {
            Destroy(tileNote3);
            score.amountToAdd = ScoreOut3(prefState3);
            prefState3 = 0;
            canHit3 = false;
            i = 3;
        }

        if (canHit4 && prefState4 != 0)
        {
            Destroy(tileNote4);
            score.amountToAdd = ScoreOut4(prefState4);
            prefState4 = 0;
            canHit4 = false;
            i = 0;
        }
    }

    // Score output for prefab states.
    #region Score Output
    // Score output for prefab 1.
    private int ScoreOut1(int num)
    {
        switch (num)
        {
            case 1:
                return 15;
            case 2:
                return 25;
            case 3:
                return 5;
            default:
                return 0;

        }
    }

    // Score output for prefab 2.
    private int ScoreOut2(int num)
    {
        switch (num)
        {
            case 1:
                return 15;
            case 2:
                return 25;
            case 3:
                return 5;
            default:
                return 0;

        }
    }

    // Score output for prefab 3.
    private int ScoreOut3(int num)
    {
        switch (num)
        {
            case 1:
                return 15;
            case 2:
                return 25;
            case 3:
                return 5;
            default:
                return 0;

        }
    }

    // Score output for prefab 4.
    private int ScoreOut4(int num)
    {
        switch (num)
        {
            case 1:
                return 15;
            case 2:
                return 25;
            case 3:
                return 5;
            default:
                return 0;

        }
    }
    #endregion
}
