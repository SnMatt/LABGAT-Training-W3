using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnVisual;
    [SerializeField] private GameObject _stoveParticles;

    private void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
    }

    private void StoveCounterOnStateChanged(object sender, StoveCounter.OnStateCHangedEventArgs e)
    {
        bool showVisual = e.State == StoveCounter.STATE.Frying || e.State == StoveCounter.STATE.Fried;
        _stoveOnVisual.SetActive(showVisual);
        _stoveParticles.SetActive(showVisual);
    }
}
