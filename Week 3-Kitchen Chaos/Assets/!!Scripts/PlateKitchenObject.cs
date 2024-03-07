using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientsAddedEventArgs> OnIngredientsAdded;
    public class OnIngredientsAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> _validObjects;

    private List<KitchenObjectSO> _kitchenObjectSOList;

    private void Awake()
    {
        _kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredients(KitchenObjectSO kitchenObjectSO)
    {
        if(!_validObjects.Contains(kitchenObjectSO))
        {
            return false;
        }

        if(_kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }else
        {
            _kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientsAdded?.Invoke(this, new OnIngredientsAddedEventArgs { KitchenObjectSO = kitchenObjectSO });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOs()
    {
        return _kitchenObjectSOList;
    }
}
