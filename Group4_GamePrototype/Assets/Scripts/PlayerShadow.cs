using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public PlayerController player;
    public float scaleFactor;
    public GameObject selfObject;
    private float sizeStart = 1;
    private float scale = 0.025f;
    private float posX;
    private float posZ;

    void Start()
    {
        posX = player.transform.position.x;
        posZ = player.transform.position.z;
}
    
    void Update()
    {
        posX = player.transform.position.x;
        transform.position = new Vector3(posX, 0, posZ);
        
        if (player.isOnGround == false && player.transform.position.y >= player.highestPos.y)
        {
            selfObject.gameObject.transform.localScale += new Vector3(scale, scale, scale);
        }
        else
        if (player.transform.position.y < player.highestPos.y && player.transform.position.y != 0.5f)
        {
            selfObject.gameObject.transform.localScale -= new Vector3(scale, scale, scale);
        }
        else
        {
            selfObject.gameObject.transform.localScale = new Vector3(sizeStart, sizeStart, sizeStart);
        }

        #region Game Over
        if (player.gameOver)
        {
            Destroy(selfObject);
        }
        #endregion
    }
}
