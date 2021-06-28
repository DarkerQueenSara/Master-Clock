using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [HideInInspector]public static PlayerEntity instance { get; private set; }
    [HideInInspector] public PlayerMovement movement { get; private set; }
    [HideInInspector]public PlayerHealth health { get; private set; }
    [HideInInspector]public PlayerControls controller { get; private set; }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple players present in scene! Destroying...");
            Destroy(gameObject);
        }
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<PlayerControls>();
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}