using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprite;

    [SerializeField]
    private GameObject _gameOverText;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_gameOverText.activeInHierarchy == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        if (_gameOverText.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }


    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLivesImg(int lives)
    {
        _livesImage.sprite = _livesSprite[lives];
    }


    public void DisplayGameOver()
    {
        _gameOverText.SetActive(true);
    }
}
