using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscRotation : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }
}
