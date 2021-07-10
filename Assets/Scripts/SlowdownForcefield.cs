using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class SlowdownForcefield : MonoBehaviour
{
    private AudioManager _audioManager;

    public void Start()
    {
        _audioManager = GetComponent<AudioManager>();
        _audioManager.Play("Spawn");
    }
}
