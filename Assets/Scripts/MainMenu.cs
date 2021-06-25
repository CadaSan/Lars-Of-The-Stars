using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _creditsText;
    [SerializeField]
    private GameObject _creditsButton;
    [SerializeField]
    private GameObject _titleScreenImage;
    [SerializeField]
    private GameObject _backButton;
    [SerializeField]
    private GameObject _howToText;
    [SerializeField]
    private GameObject _howToButton;


    public void Start()
    {
        _creditsText.SetActive(false);
        _backButton.SetActive(false);
        _howToText.SetActive(false);
        _creditsButton.SetActive(true);
        _howToButton.SetActive(true);
        _titleScreenImage.SetActive(true);
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void DisplayCredits()
    {
        _titleScreenImage.SetActive(false);
        _creditsButton.SetActive(false);
        _howToText.SetActive(false);
        _backButton.transform.position = _creditsButton.transform.position;
        _howToButton.SetActive(true);
        _creditsText.SetActive(true);
        _backButton.SetActive(true);

    }

    public void DisplayHowTo()
    {
        _titleScreenImage.SetActive(false);
        _howToButton.SetActive(false);
        _creditsText.SetActive(false);
        _backButton.transform.position = _howToButton.transform.position;
        _creditsButton.SetActive(true);
        _howToText.SetActive(true);
        _backButton.SetActive(true);
    }

    public void DisplayTitleScreen()
    {
        _creditsText.SetActive(false);
        _howToText.SetActive(false);
        _backButton.SetActive(false);
        _titleScreenImage.SetActive(true);
        _creditsButton.SetActive(true);
        _howToButton.SetActive(true);
        
    }
}
