using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFootActions;

    private PlayerMovement playerMovement;
    private PlayerCamera playerCamera;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;

        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();

        onFootActions.Jump.performed += ctx => playerMovement.Jump();
        onFootActions.Crouch.performed += ctx => playerMovement.Crouch();
        onFootActions.Sprint.performed += ctx => playerMovement.Sprint();
    }

    private void FixedUpdate()
    {
        playerMovement.ProcessMovement(onFootActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerCamera.ProcessLook(onFootActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFootActions.Enable();
    }

    private void OnDisable()
    {
        onFootActions.Disable();
    }
}
