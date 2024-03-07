using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;

        Hide();
    }

    private void Update()
    {
        if(GameManager.Instance.GameState == GameManager.GAMESTATE.Countdown)
        {
            _countdownText.text = GameManager.Instance.GetCountdownTimer().ToString("F0");
        }
    }
    private void GameManagerOnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.GameState == GameManager.GAMESTATE.Countdown)
        {
            Show();
        }else
        {
            Hide();
        }
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
