using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //private AudioManager _audioManager;

    #region SingleTon

    public static GameManager Instance { get; private set; }

    [HideInInspector] public int lastLoops;
    [HideInInspector] public int lastPercentage;
    [HideInInspector] public float lastPlaytime;
    [HideInInspector] public int lastEnding;

    private void Awake()
    {
        // Needed if we want the audio manager to persist through scenes
        if (Instance == null)
        {
            Instance = this;
            //_audioManager = AudioManager.Instance;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public void ResetGame()
    {
        lastLoops = 0;
        lastEnding = 0;
        lastPercentage = 0;
        lastPlaytime = 0.0f;
    }
    
}
