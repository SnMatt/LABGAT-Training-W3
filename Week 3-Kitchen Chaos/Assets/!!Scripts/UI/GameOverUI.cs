using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ordersDeliveredText;
    [SerializeField] private Button _mainMenuButton;


    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            Loader.LoadScene("MainMenu");
        });
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;

        Hide();
    }

    private void GameManagerOnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.GameState == GameManager.GAMESTATE.GameOver)
        {
            Show();
            _ordersDeliveredText.text = "Orders Completed: " + DeliveryManager.Instance.GetOrderCompletedAmount();
        }
        else
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
