using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter _plateCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;

    private List<GameObject> _plateVisualGO;

    private void Awake()
    {
        _plateVisualGO = new List<GameObject>();
    }

    private void Start()
    {
        _plateCounter.OnPlateSpawned += PlateCounterOnPlateSpawned;
        _plateCounter.OnPlateRemoved += PlateCounterOnPlateRemoved;
    }

    private void PlateCounterOnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGO = _plateVisualGO[_plateVisualGO.Count - 1];
        _plateVisualGO.Remove(plateGO);
        Destroy(plateGO);
    }

    private void PlateCounterOnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTf = Instantiate(_plateVisualPrefab, _counterTopPoint);

        plateVisualTf.localPosition = new Vector3(0, 0.1f * _plateVisualGO.Count, 0);

        _plateVisualGO.Add(plateVisualTf.gameObject);
    }
}
