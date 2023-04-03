using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingAnimation : MonoBehaviour
{
    private float initialZ; // The initial value of the enemy cube's Z position is stored as a separate variable for organization purposes and acts as a safe reference.
    private float alpha;
    private float timer;
    private float timeout;
    private Color outerColor1;
    private Color outerColor2;
    private Color innerColor;
    private bool color1;
    public float t; // How transparent the color is.
    private SpriteRenderer selfRenderer;

    #region Setup
    void Awake() // Awake() is called before Start() and is possibly the only way for this coordinate conversion system to work.
    {
        initialZ = transform.position.z; // Represents full value to be split into two.
        timer = Mathf.Floor(initialZ); // This essentially eliminates the decimal from the original value and stores it as the time.
        alpha = initialZ - timer; // This essentially takes only the decimal and uses it to see whether the enemy shadow should be one color or the other. This value can only ever be equal to either 0 or 0.5f.
    }

    void Start()
    {
        selfRenderer = GetComponent<SpriteRenderer>();
        outerColor1 = new Color(0, 255, 0, t);
        outerColor2 = new Color(0, 135, 255, t);
        innerColor = new Color(0, 255, 255, t);
        timeout = 0.4f;
        if (alpha == 0.5f) // A value of 0.5f essentially returns a value of true.
        {
            if (t >= 0.2f)
            {
                selfRenderer.color = outerColor1;
            }
            else
            {
                selfRenderer.color = innerColor;
            }
            color1 = true;
        }
        else
        {
            if (t >= 0.2f)
            {
                selfRenderer.color = outerColor2;
            }
            else
            {
                selfRenderer.color = innerColor; ;
            }
            color1 = false;
        }
    }
    #endregion

    // Every 0.4 seconds, the enemy shadow's alpha value updates. If it is currently one color, then it will switch over to the other color.
    void Update()
    {
        timer += Time.deltaTime; // Timer.
        // When the timer is equal to the max amount of time set, reset timer and either become opaque or transparent.
        if (timer > timeout)
        {
            if (color1)
            {
                selfRenderer.color = new Color(selfRenderer.color.b, selfRenderer.color.r, selfRenderer.color.g);
                color1 = false;
            }
            else
            {
                selfRenderer.color = new Color(selfRenderer.color.r, selfRenderer.color.g, selfRenderer.color.b);
                color1 = true;
            }
            timer = 0; // Resets timer.
        }
    }
}
