using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerReader : MonoBehaviour
{  
    private PlayerController _controller;
    private float _moveDuration;
    private bool _isShoot;

    public float MoveDuration => _moveDuration;
    public bool IsShoot => _isShoot;

    private void Awake()
    {
        _controller = new PlayerController();
    }

    private void OnEnable()
    {
        _controller.Movement.Enable();
        _controller.Movement.Move.canceled += OnMove;
        _controller.Movement.Move.performed += OnMove;
    }

    private void OnDisable()
    {
        _controller.Movement.Move.canceled -= OnMove;
        _controller.Movement.Move.performed -= OnMove;
        _controller.Movement.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveDuration = context.ReadValue<float>();
    }
}
