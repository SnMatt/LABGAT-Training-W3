using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _counterGameObject;
    [SerializeField] private Image _barImage;

    private IHasProgress _progressCounter;

    private void Start()
    {
        _progressCounter = _counterGameObject.GetComponent<IHasProgress>();

        _progressCounter.OnProgressChanged += CounterOnProgressChanged;
        _barImage.fillAmount = 0f;

        Hide();
    }

    private void CounterOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _barImage.fillAmount = e.ProgressNormalized;

        if(e.ProgressNormalized == 0f || e.ProgressNormalized == 1f)
        {
            Hide();
        }else
        {
            Show();
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
