using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;

    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Interact.performed += InteractOnPerformed;
        _playerInputAction.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
    }

    private void InteractAlternateOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementVector3Normalized()
    {
        return _playerInputAction.Player.Move.ReadValue<Vector3>();
    }
}