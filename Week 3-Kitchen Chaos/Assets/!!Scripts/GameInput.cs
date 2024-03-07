using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputAction _playerInputAction;

    public enum BINDING
    {
        MoveUp,
        MoveLeft,
        MoveDown,
        MoveRight,
        Interact,
        Alt
    };
    private void Awake()
    {
        Instance = this;

        _playerInputAction = new PlayerInputAction();

        if (PlayerPrefs.HasKey("InputBindings"))
        {
            _playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString("InputBindings"));
        }

        _playerInputAction.Player.Enable();

        _playerInputAction.Player.Interact.performed += InteractPerformed;
        _playerInputAction.Player.InteractAlternate.performed += InteractAlternatePerformed;
        _playerInputAction.Player.Pause.performed += PausePerformed;


    }
    private void OnDestroy()
    {
        _playerInputAction.Player.Interact.performed -= InteractPerformed;
        _playerInputAction.Player.InteractAlternate.performed -= InteractAlternatePerformed;
        _playerInputAction.Player.Pause.performed -= PausePerformed;

        _playerInputAction.Dispose();
    }

    private void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternatePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetTextBinding(BINDING binding)
    {
        switch (binding)
        {
            case BINDING.MoveUp:
                return _playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case BINDING.MoveLeft:
                return _playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case BINDING.MoveDown:
                return _playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case BINDING.MoveRight:
                return _playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case BINDING.Interact:
                return _playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case BINDING.Alt:
                return _playerInputAction.Player.InteractAlternate.bindings[0].ToDisplayString();
            default:
                return _playerInputAction.Player.Interact.bindings[0].ToDisplayString();
        }
    }

    public void SetBinding(BINDING binding, Action onActionRebound)
    {
        _playerInputAction.Player.Disable();


        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default:
            case BINDING.MoveUp:
                inputAction = _playerInputAction.Player.Move;
                bindingIndex = 1;
                break;
            case BINDING.MoveLeft:
                inputAction = _playerInputAction.Player.Move;
                bindingIndex = 3;
                break;
            case BINDING.MoveDown:
                inputAction = _playerInputAction.Player.Move;
                bindingIndex = 2;
                break;
            case BINDING.MoveRight:
                inputAction = _playerInputAction.Player.Move;
                bindingIndex = 4;
                break;
            case BINDING.Interact:
                inputAction = _playerInputAction.Player.Interact;
                bindingIndex = 0;
                break;
            case BINDING.Alt:
                inputAction = _playerInputAction.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            _playerInputAction.Player.Enable();
            onActionRebound();

            ;
            PlayerPrefs.SetString("InputBindings", _playerInputAction.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
}
