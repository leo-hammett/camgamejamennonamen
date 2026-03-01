using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class MenuUIController : MonoBehaviour
{
    [Header("References")]
    private GameObject menuCanvas;
    [SerializeField] private Button playButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image logo;
    public bool playing = false;
    public float startTime;
    public event System.Action StartGame;
    public event System.Action Died;

    void Awake()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
        
        // Ensure UI scales properly
        if (menuCanvas != null && menuCanvas.GetComponent<SimpleUIScaler>() == null)
        {
            menuCanvas.AddComponent<SimpleUIScaler>();
        }
    }

    void Start()
    {
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayClicked);
        else
            Debug.LogWarning("Play button is not assigned in MenuUIController!");
            
        if (logo == null)
            Debug.LogWarning("Logo image is not assigned in MenuUIController!");
            
        if (retryButton == null)
            Debug.LogWarning("Retry button is not assigned in MenuUIController!");
            
        if (scoreText == null)
            Debug.LogWarning("Score text is not assigned in MenuUIController!");
    }

    void OnPlayClicked()
    {
        if (logo != null)
            logo.gameObject.SetActive(false);
        if (playButton != null)
            playButton.gameObject.SetActive(false);
        startTime = Time.time;
        playing = true;
        StartGame?.Invoke();
    }

    void OnRetryClicked()
    {
        if (retryButton != null)
            retryButton.gameObject.SetActive(false);
        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
        startTime = Time.time;
        playing = true;
        StartGame?.Invoke();
    }

    public void EndGame()
    {
        playing = false;
        Died?.Invoke();
        
        if (retryButton != null)
        {
            retryButton.gameObject.SetActive(true);
            retryButton.onClick.RemoveAllListeners(); // Clear existing listeners
            retryButton.onClick.AddListener(OnRetryClicked);
        }
        
        int score = (int) math.round(Time.time - startTime)*100;
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            scoreText.gameObject.SetActive(true);
        }
    }
}
