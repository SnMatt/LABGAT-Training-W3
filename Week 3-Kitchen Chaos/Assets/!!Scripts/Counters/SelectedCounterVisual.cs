using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _Counter;
    [SerializeField] private GameObject[] _selectedVisuals;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.SelectedCounter == _Counter)
        {
            Show();
        }else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var item in _selectedVisuals)
        {
            item.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (var item in _selectedVisuals)
        {
            item.SetActive(false);
        }
    }


}
