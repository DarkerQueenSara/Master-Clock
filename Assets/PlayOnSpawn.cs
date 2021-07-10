using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnSpawn : MonoBehaviour
{
    public String soundName;

    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GetComponent<AudioManager>();
        _audioManager.Play(soundName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
