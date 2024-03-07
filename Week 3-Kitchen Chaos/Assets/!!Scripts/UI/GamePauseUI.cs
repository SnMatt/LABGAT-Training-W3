using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{ 

    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _optionsBtn;
    [SerializeField] private Button _mainMenuBtn;

    private void Awake()
    {

        _resumeBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.PauseGame();
        });
        _optionsBtn.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
        _mainMenuBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            Loader.LoadScene("MainMenu");
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManagerOnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;

        Hide();
    }

    private void GameManagerOnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManagerOnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
