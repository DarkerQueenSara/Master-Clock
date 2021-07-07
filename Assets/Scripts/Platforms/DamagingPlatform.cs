using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        { // If player collides

            PlayerHealth playerScript = collision.gameObject.GetComponent<PlayerHealth>();
            if(playerScript.playerClock.localTimeScale != 1.0f) // If player not rewinding and not speeding up
            {

            }
        }
    }
}
