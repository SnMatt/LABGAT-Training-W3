using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Start()
    {
        _plateKitchenObject.OnIngredientsAdded += PlateKitchenObjectOnIngredientsAdded;
    }

    private void PlateKitchenObjectOnIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO item in _plateKitchenObject.GetKitchenObjectSOs())
        {
            Transform icon = Instantiate(_iconTemplate, transform);
            icon.gameObject.SetActive(true);
            icon.GetComponent<PlateIconSingleUI>().SetSprite(item);
        }
    }
}
