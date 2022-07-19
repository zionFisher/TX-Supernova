using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool KeyInteractable = true;
    public bool AxisInteractable = true;

    public KeyCode SetCameraAndMovementMode = KeyCode.V;
    public KeyCode SprintKeyCode            = KeyCode.LeftShift;
    public KeyCode JumpKeyCode              = KeyCode.Space;
    public KeyCode FireKeyCode              = KeyCode.Mouse0;
    public KeyCode AimKeyCode               = KeyCode.Mouse1;
    
    // Process Fixed Event
    private void FixedUpdate()
    {
        if (!AxisInteractable)
            return;

        // Movement
        HandleMovement();
    }

    // Process Key Event
    private void Update()
    {
        if (!KeyInteractable)
            return;

        // Camera and Movement Mode
        if (Input.GetKey(SetCameraAndMovementMode))
        {
            HandleCameraMode();
            HandleMovementMode();
        }

        // Fire
        if (Input.GetKeyDown(FireKeyCode))
        {
            HandleFireKeyDown();
        }
        if (Input.GetKeyUp(FireKeyCode))
        {
            HandleFireKeyUp();
        }

        // Aim
        if (Input.GetKeyDown(AimKeyCode))
        {
            HandleAimKeyDown();
        }
        if (Input.GetKeyUp(AimKeyCode))
        {
            HandleAimKeyUp();
        }
    }

    private void HandleMovement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isSprinting = Input.GetKey(SprintKeyCode) && moveInput != Vector2.zero;

        PlayerManager.Instance.PlayerMove(moveInput);
        PlayerManager.Instance.PlayerSprint(isSprinting);
    }

    private void HandleCameraMode()
    {
        CameraManager.Instance.ChangeCharacterCameraMode();
        StartCoroutine(Utility.InvokeBeforeAndAfterSecondes(2.0f, () => { KeyInteractable = false; }, () => { KeyInteractable = true; }));
    }

    private void HandleMovementMode()
    {
        PlayerManager.Instance.ChangePlayerMoveMode();
    }

    private void HandleJumpKey()
    {
        // TODO:
    }

    private void HandleFireKeyDown()
    {
        // TODO:
    }

    private void HandleFireKeyUp()
    {
        // TODO:
    }

    private void HandleAimKeyDown()
    {
        CameraManager.Instance.CameraAim(true);
        PlayerManager.Instance.PlayerAim(true);
    }

    private void HandleAimKeyUp()
    {
        CameraManager.Instance.CameraAim(false);
        PlayerManager.Instance.PlayerAim(false);
    }
}