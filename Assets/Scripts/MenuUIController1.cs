using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace InGraved.UI
{
    public class MenuUIController : MonoBehaviour
    {
        [Header("References")]
        private GameObject menuCanvas;
        private Button playButton;

        void Awake()
        {
            
        }

        void Start()
        {
            playButton.onClick.AddListener(OnPlayClicked);
        }

        void OnPlayClicked()
        {
            menuCanvas.SetActive(false);
            
        }
    }
}
