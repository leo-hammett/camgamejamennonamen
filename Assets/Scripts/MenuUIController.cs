using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [Header("References")]
    private GameObject menuCanvas;
    [SerializeField] private Button playButton;
    public bool playing = false;
    public float startTime;
    public event System.Action StartGame;

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
        menuCanvas.SetActive(false);
        startTime = Time.time;
        playing = true;
        StartGame?.Invoke();
    }
}
