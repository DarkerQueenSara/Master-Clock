using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRenderer : MonoBehaviour
{
    public float rotationSpeed;
    public bool clockwise;

    public void Update()
    {
        float angleDiff = rotationSpeed * Time.deltaTime;
        if (!clockwise) angleDiff *= 1;
        transform.Rotate(new Vector3(0, 0, angleDiff));
 
    }
}
