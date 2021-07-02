using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.IsChildOf(gameObject.transform.parent))
        {
            Destroy(other.gameObject);
        }
    }
}
