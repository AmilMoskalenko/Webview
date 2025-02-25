using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _textImage;
    [SerializeField] private List<Sprite> _textSprites;
    [SerializeField] private List<GameObject> _labelsObj;
    [SerializeField] private Button _peachButton;
    [SerializeField] private Button _lavandaButton;
    [SerializeField] private Button _yellowButton;
    [SerializeField] private Button _mintButton;

    private int _index;
    private int _indexL;

    private void Start()
    {
        ChangeGame();
        _peachButton.onClick.AddListener(() => Peach());
        _lavandaButton.onClick.AddListener(() => Lavanda());
        _yellowButton.onClick.AddListener(() => Yellow());
        _mintButton.onClick.AddListener(() => Mint());
    }

    private void ChangeGame()
    {
        System.Random rand = new System.Random();
        _index = rand.Next(_textSprites.Count);
        _textImage.sprite = _textSprites[_index];
        _textImage.SetNativeSize();

        System.Random randL = new System.Random();
        _indexL = randL.Next(_labelsObj.Count);
        _labelsObj.ForEach(obj => obj.SetActive(false));
        _labelsObj[_indexL].SetActive(true);
    }

    private void Peach()
    {
        if ((_indexL == 0 && (_index == 3 || _index == 6 || _index == 9)) ||
            (_indexL == 1 && (_index == 0 || _index == 1 || _index == 2)))
        {
            ChangeGame();
        }
    }

    private void Lavanda()
    {
        if ((_indexL == 0 && (_index == 0 || _index == 7 || _index == 10)) ||
            (_indexL == 1 && (_index == 3 || _index == 4 || _index == 5)))
        {
            ChangeGame();
        }
    }

    private void Yellow()
    {
        if ((_indexL == 0 && (_index == 1 || _index == 4 || _index == 11)) ||
            (_indexL == 1 && (_index == 6 || _index == 7 || _index == 8)))
        {
            ChangeGame();
        }
    }

    private void Mint()
    {
        if ((_indexL == 0 && (_index == 2 || _index == 5 || _index == 8)) ||
            (_indexL == 1 && (_index == 9 || _index == 10 || _index == 11)))
        {
            ChangeGame();
        }
    }
}
