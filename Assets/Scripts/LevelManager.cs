using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region SingleTon

    public static LevelManager Instance { get; private set; }

    [HideInInspector] public int loops;
    [HideInInspector] public int collectedItems;

    private float _playTime;

    private GameManager _gameManager;
    private AudioManager _audioManager;
     
    private void Awake()
    {
        // Needed if we want the audio manager to persist through scenes
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void Start()
    {
        _playTime -= Time.time;
        loops = 0;
        collectedItems = 0;
        _gameManager = GameManager.Instance;
        _audioManager = GetComponent<AudioManager>();
        _audioManager.Play("LevelMusic");
    }
    
    public void EndGame()
    {
        _playTime += Time.time;
        //-1 porque isto corre sempre 1 no inicio do jogo
        _gameManager.lastLoops = loops - 1;
        _gameManager.lastPercentage = Mathf.CeilToInt(collectedItems / 12 * 100);
        _gameManager.lastPlaytime = _playTime;
        int x = 0;
        if (loops - 1 == 0)
        {
            x = collectedItems == 12 ? 2 : 1;
        }

        _gameManager.lastEnding = x;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}