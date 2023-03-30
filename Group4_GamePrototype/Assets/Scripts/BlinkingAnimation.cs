using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingAnimation : MonoBehaviour
{
    private float initialZ; // The initial value of the enemy cube's Z position is stored as a separate variable for organization purposes and acts as a safe reference.
    private float alpha;
    private float timer;
    private float timeout;
    public float t; // Opacity value for when the enemy shadow is visible.
    private SpriteRenderer selfRenderer;

    #region Setup
    void Awake() // Awake() is called before Start() and is possibly the only way for this coordinate conversion system to work.
    {
        initialZ = transform.position.z; // Represents full value to be split into two.
        timer = Mathf.Floor(initialZ); // This essentially eliminates the decimal from the original value and stores it as the time.
        alpha = initialZ - timer; // This essentially takes only the decimal and uses it to see whether the enemy shadow should be transparent or opaque. This value can only ever be equal to either 0 or 0.5f.
    }

    void Start()
    {
        selfRenderer = GetComponent<SpriteRenderer>();
        timeout = 0.4f;
        if (alpha == 0.5f) // A value of 0.5f essentially returns a value of true, which means the enemy shadow will start off as opaque.
        {
            selfRenderer.color = new Color(selfRenderer.color.r, selfRenderer.color.g, selfRenderer.color.b, t);
        }
        else
        {
            selfRenderer.color = new Color(selfRenderer.color.r, selfRenderer.color.g, selfRenderer.color.b, 0);
        }
    }
    #endregion

    // Every 0.4 seconds, the enemy shadow's alpha value updates. If it is currently transparent, then it will become opaque, and vice versa.
    void Update()
    {
        timer += Time.deltaTime; // Timer.
        // When the timer is equal to the max amount of time set, reset timer and either become opaque or transparent.
        if (timer > timeout)
        {
            if (selfRenderer.color.a == t)
            {
                selfRenderer.color = new Color(selfRenderer.color.r, selfRenderer.color.g, selfRenderer.color.b, 0);
            }
            else
            {
                selfRenderer.color = new Color(selfRenderer.color.r, selfRenderer.color.g, selfRenderer.color.b, t);
            }
            timer = 0; // Resets timer.
        }
    }
}
