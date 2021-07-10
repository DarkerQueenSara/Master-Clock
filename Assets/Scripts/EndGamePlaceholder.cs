using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePlaceholder : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerEntity.instance.colliders.Contains(other.gameObject))
        {
            LevelManager.Instance.EndGame();
        }
    }
}
