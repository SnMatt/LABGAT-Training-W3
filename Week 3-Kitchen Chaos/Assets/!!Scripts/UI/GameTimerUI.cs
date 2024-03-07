using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private Image _gameTimer;


    private void Update()
    {
        _gameTimer.fillAmount = GameManager.Instance.GetPlayTimerNormalized();
    }
}
