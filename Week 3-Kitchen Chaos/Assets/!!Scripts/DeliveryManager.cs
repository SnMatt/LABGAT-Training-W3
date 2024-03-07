using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    [SerializeField] private RecipeListSO _recipeListSO;
    private List<RecipeSO> _waitingRecipeSOList;

    private float _spawnRecipeTimer;
    private float _spawnRecipeCD = 5f;
    private int _waitingRecipeLimit = 4;
    private int _orderCompletedAmount = 0;
    private void Awake()
    {
        _waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
        _orderCompletedAmount = 0;
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if(_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = _spawnRecipeCD;

            if(_waitingRecipeSOList.Count < _waitingRecipeLimit)
            {
                RecipeSO recipeSO = _recipeListSO.RecipeSOList[UnityEngine.Random.Range(0, _recipeListSO.RecipeSOList.Count)];
                _waitingRecipeSOList.Add(recipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitRecipe = _waitingRecipeSOList[i];

            if(waitRecipe.KitchenObjectsSO.Count == plateKitchenObject.GetKitchenObjectSOs().Count)
            {
                bool plateContentMatchRecipe = true;
                foreach (KitchenObjectSO kitchenObjectSO in waitRecipe.KitchenObjectsSO)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOs())
                    {
                        if(plateKitchenObjectSO == kitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        plateContentMatchRecipe = false;
                    }
                }

                if(plateContentMatchRecipe)
                {
                    _waitingRecipeSOList.RemoveAt(i);

                    _orderCompletedAmount++;
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
    }


    public List<RecipeSO> GetWaitingRecipeSOs()
    {
        return _waitingRecipeSOList;
    }
    public int GetOrderCompletedAmount()
    {
        return _orderCompletedAmount;
    }
}
