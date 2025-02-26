using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private List<GameObject> _errorsObj;
    [SerializeField] private Image _textImage;
    [SerializeField] private List<Sprite> _textSprites;
    [SerializeField] private List<GameObject> _labelsObj;
    [SerializeField] private Button _peachButton;
    [SerializeField] private Button _lavandaButton;
    [SerializeField] private Button _yellowButton;
    [SerializeField] private Button _mintButton;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _leaveGameButton;
    [SerializeField] private float _timer;
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private float _minTimer;
    [SerializeField] private float _timerStep;
    [SerializeField] private GameObject _failScreen;
    [SerializeField] private TextMeshProUGUI _failScoreText;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _mainMenuButton;

    private int _index;
    private int _indexL;
    private int _errors;
    private int _score;
    private Coroutine _timerCoroutine;

    private void Start()
    {
        ChangeGame();
        _pauseButton.onClick.AddListener(() => Pause(true));
        _peachButton.onClick.AddListener(() => ButtonClick(3, 6, 9, 0, 1, 2));
        _lavandaButton.onClick.AddListener(() => ButtonClick(0, 7, 10, 3, 4, 5));
        _yellowButton.onClick.AddListener(() => ButtonClick(1, 4, 11, 6, 7, 8));
        _mintButton.onClick.AddListener(() => ButtonClick(2, 5, 8, 9, 10, 11));
        _continueButton.onClick.AddListener(() => Pause(false));
        _leaveGameButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        _retryButton.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
    }

    private void ChangeGame()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }

        System.Random rand = new System.Random();
        _index = rand.Next(_textSprites.Count);
        _textImage.sprite = _textSprites[_index];
        _textImage.SetNativeSize();

        System.Random randL = new System.Random();
        _indexL = randL.Next(_labelsObj.Count);
        _labelsObj.ForEach(obj => obj.SetActive(false));
        _labelsObj[_indexL].SetActive(true);

        _timerSlider.maxValue = _timer;
        _timerSlider.value = _timer;

        _timerCoroutine = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        float timeRemaining = _timer;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            _timerSlider.value = timeRemaining;
            yield return null;
        }
        Error();
        ChangeGame();
    }

    private void Pause(bool pause)
    {
        _pauseScreen.SetActive(pause);
        Time.timeScale = pause ? 0 : 1;
    }

    private void ButtonClick(int indexColor1, int indexColor2, int indexColor3,
        int indexText1, int indexText2, int indexText3)
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }
        if ((_indexL == 0 && (_index == indexColor1 || _index == indexColor2 || _index == indexColor3)) ||
            (_indexL == 1 && (_index == indexText1 || _index == indexText2 || _index == indexText3)))
        {
            Score();
        }
        else
        {
            Error();
        }
        ChangeGame();
    }

    private void Error()
    {
        _errors++;
        _errorsObj.ForEach(obj => obj.SetActive(_errorsObj.IndexOf(obj) >= _errors));
        if (_errors == _errorsObj.Count)
        {
            _failScreen.SetActive(true);
            _failScoreText.text = _score.ToString();
            PlayerPrefs.SetInt("Score", _score);
        }
    }

    private void Score()
    {
        _score++;
        _scoreText.text = _score.ToString();
        _timer = Mathf.Max(_minTimer, _timer - _timerStep);
    }
}
