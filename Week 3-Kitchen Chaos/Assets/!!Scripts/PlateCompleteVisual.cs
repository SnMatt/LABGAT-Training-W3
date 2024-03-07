using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GO
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }

    [SerializeField] PlateKitchenObject _plateKitchenObject;
    [SerializeField] List<KitchenObjectSO_GO> _kitchenObjectSOGOList;

    private void Start()
    {
        _plateKitchenObject.OnIngredientsAdded += PlateKitchenObjectOnIngredientsAdded;
        foreach (KitchenObjectSO_GO item in _kitchenObjectSOGOList)
        {
            item.GameObject.SetActive(false);
        }
    }

    private void PlateKitchenObjectOnIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GO item in _kitchenObjectSOGOList)
        {
            if(item.KitchenObjectSO == e.KitchenObjectSO)
            {
                item.GameObject.SetActive(true);
            }
        }
    }
}
