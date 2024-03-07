using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateCHangedEventArgs> OnStateChanged;
    public class OnStateCHangedEventArgs : EventArgs
    {
        public STATE State;
    }

    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] _burnRecipeSOArray;

    public enum STATE
    {
        Idle,
        Frying,
        Fried,
        Burnt
    };
    private STATE State;

    private float _fryTimer;
    private float _burningTimer;
    private FryingRecipeSO _fryingRecipeSO;
    private BurningRecipeSO _burningRecipeSO;

    private void Start()
    {
        State = STATE.Idle;
    }
    private void Update()
    {
        if(HasKitchenObject())
        {
            switch (State)
            {
                case STATE.Idle:
                    break;
                case STATE.Frying:
                    _fryTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { ProgressNormalized = _fryTimer / _fryingRecipeSO.TimeProgressMax });

                    if (_fryTimer > _fryingRecipeSO.TimeProgressMax)
                    {
                        GetKitchenObject().SelfDestroy();

                        KitchenObject.SpawnKitchenObject(_fryingRecipeSO.Output, this);

                        State = STATE.Fried;
                        OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs { State = State });
                        _burningTimer = 0;
                        _burningRecipeSO = GetBurnRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    }

                    break;
                case STATE.Fried:
                    _burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { ProgressNormalized = _burningTimer / _burningRecipeSO.TimeBurnProgressMax });

                    if (_burningTimer > _burningRecipeSO.TimeBurnProgressMax)
                    {
                        GetKitchenObject().SelfDestroy();

                        KitchenObject.SpawnKitchenObject(_burningRecipeSO.Output, this);

                        State = STATE.Burnt;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { ProgressNormalized = 0f });
                        OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs { State = State });
                    }
                    break;
                case STATE.Burnt:
                    break;
                default:
                    break;
            }


            
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _fryingRecipeSO = GetFryRecipeSO(GetKitchenObject().GetKitchenObjectSO());

                    _fryTimer = 0;
                    State = STATE.Frying;
                    OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs { State = State });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { ProgressNormalized = _fryTimer / _fryingRecipeSO.TimeProgressMax });
                }

            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                State = STATE.Idle;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { ProgressNormalized = 0f });
                OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs { State = State });
            }else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().SelfDestroy();

                        State = STATE.Idle;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { ProgressNormalized = 0f });
                        OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs { State = State });
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryRecipeSO(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryRecipeSO(kitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.Output;
        }
        else
            return null;
    }

    private FryingRecipeSO GetFryRecipeSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO item in _fryingRecipeSOArray)
        {
            if (item.Input == kitchenObjectSO)
            {
                return item;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurnRecipeSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurningRecipeSO item in _burnRecipeSOArray)
        {
            if (item.Input == kitchenObjectSO)
            {
                return item;
            }
        }
        return null;
    }
}
