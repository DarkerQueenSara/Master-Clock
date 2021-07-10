using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePlaceholder : MonoBehaviour
{
    public CloneEnemySpawner bossSpawner;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerEntity.instance.colliders.Contains(other.gameObject))
        {
            if(LevelManager.Instance.loops <= 1)
            { // If we never looped, just end the game
                LevelManager.Instance.EndGame();
            }else if (!bossSpawner.enabled)
            { // Else enable the boss spawner
                bossSpawner.EnableSpawner();
            }
        }
    }
}
