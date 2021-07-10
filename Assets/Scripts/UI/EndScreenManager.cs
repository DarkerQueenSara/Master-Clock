using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public GameObject endScreen, creditsScreen;

    public Button playButton, exitButton, backButton;

    public TextMeshProUGUI loopsText, percentageText, playTimeText;

    private GameManager _gameManager;

    //private AudioManager _audioManager;
    private void Start()
    {
        //_audioManager = GetComponent<AudioManager>();
        _gameManager = GameManager.Instance;
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        backButton.onClick.AddListener(HideStats);
        // _audioManager.Play("TitleScreen");

        loopsText.text = "Loops: " + _gameManager.lastLoops;
        percentageText.text = "Item Collected: " + _gameManager.lastPercentage + "%";
        float minutes = Mathf.FloorToInt(_gameManager.lastPlaytime / 60);
        float seconds = Mathf.FloorToInt(_gameManager.lastPlaytime % 60);
        playTimeText.text = "Play Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void HideStats()
    {
        creditsScreen.SetActive(true);
        endScreen.SetActive(false);
    }
}