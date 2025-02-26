using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _settingsScreen;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        _scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
        _playButton.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        _settingsButton.onClick.AddListener(() => _settingsScreen.SetActive(true));
        _closeButton.onClick.AddListener(() => _settingsScreen.SetActive(false));
    }
}
