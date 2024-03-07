using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO _plateKitchenObjectSO;

    private float _spawnPlateTimer;
    private float _spawnCD = 4f;
    private int _plateSpawned;
    private int _plateMaxSpawn = 4;

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if(_spawnPlateTimer > _spawnCD)
        {
            _spawnPlateTimer = 0f;
            if(_plateSpawned < _plateMaxSpawn)
            {
                _plateSpawned++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            if(_plateSpawned > 0)
            {
                _plateSpawned--;

                KitchenObject.SpawnKitchenObject(_plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
