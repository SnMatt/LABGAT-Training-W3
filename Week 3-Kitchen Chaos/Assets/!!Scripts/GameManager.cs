using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public enum GAMESTATE
    {
        Wait,
        Countdown,
        Playing,
        GameOver
    };
    public GAMESTATE GameState { get; private set; }

    private float _waitToStartTimer = 0.5f;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer;
    private float _gamePlayingTimerMax = 25f;

    private bool _isGamePaused;

    private void Awake()
    {
        Instance = this;
        GameState = GAMESTATE.Wait;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameManagerOnPauseAction;
    }

    private void GameManagerOnPauseAction(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void Update()
    {
        switch (GameState)
        {
            case GAMESTATE.Wait:
                _waitToStartTimer -= Time.deltaTime;
                if(_waitToStartTimer <= 0)
                {
                    GameState = GAMESTATE.Countdown;
                }

                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
            case GAMESTATE.Countdown:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer <= 0)
                {
                    _gamePlayingTimer = _gamePlayingTimerMax;
                    GameState = GAMESTATE.Playing;
                }

                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
            case GAMESTATE.Playing:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer <= 0)
                {
                    GameState = GAMESTATE.GameOver;
                }

                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
            case GAMESTATE.GameOver:

                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
            default:
                break;
        }
    }

    public void PauseGame()
    {
        _isGamePaused = !_isGamePaused;
        if(_isGamePaused)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }else
        {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePlaying()
    {
        return GameState == GAMESTATE.Playing;
    }

    public float GetCountdownTimer()
    {
        return _countdownToStartTimer;
    }
    public float GetPlayTimerNormalized()
    {
        return (_gamePlayingTimer / _gamePlayingTimerMax);
    }
}
