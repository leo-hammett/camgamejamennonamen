using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [Header("References")]
    private GameObject menuCanvas;
    [SerializeField] private Button playButton;
    [SerializeField] private Button retryButton;
    public bool playing = false;
    public float startTime;
    public event System.Action StartGame;
    public event System.Action Died;

    void Awake()
    {
        menuCanvas = GameObject.Find("MenuCanvas");
    }

    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
    }

    void OnPlayClicked()
    {
        playButton.gameObject.SetActive(false);
        startTime = Time.time;
        playing = true;
        StartGame?.Invoke();
    }

    void OnRetryClicked()
    {
        retryButton.gameObject.SetActive(false);
        startTime = Time.time;
        playing = true;
        StartGame?.Invoke();
    }

    public void EndGame()
    {
        playing = false;
        Died?.Invoke();
        retryButton.gameObject.SetActive(true);
        retryButton.onClick.AddListener(OnRetryClicked);
    }
}
