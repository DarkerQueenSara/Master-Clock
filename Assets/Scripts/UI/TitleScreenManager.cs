using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject titleScreen, storyScreen;

    public Button playButton, storyButton, backButton;

    private GameManager _gameManager;
    private AudioManager _audioManager;
    
    private void Start()
    {
        _audioManager = GetComponent<AudioManager>();
        _gameManager = GameManager.Instance;
        playButton.onClick.AddListener(StartGame);
        storyButton.onClick.AddListener(ShowStory);
        backButton.onClick.AddListener(HideStory);
       _audioManager.Play("TitleMusic");
    }
    
    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ShowStory()
    {
        storyScreen.SetActive(true);
        titleScreen.SetActive(false);
    }

    private void HideStory()
    {
        titleScreen.SetActive(true);
        storyScreen.SetActive(false);
    }
 
}