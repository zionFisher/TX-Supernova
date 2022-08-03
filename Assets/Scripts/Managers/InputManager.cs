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

    public void StartMonitorInput()
    {
        KeyInteractable = true;
        AxisInteractable = true;
    }

    public void StopMonitorInput()
    {
        KeyInteractable = false;
        AxisInteractable = false;

        PlayerManager.Instance.PlayerStopMove();
    }

    // Process Fixed Event
    private void FixedUpdate()
    {
        if (!AxisInteractable)
            return;

        // Movement
        HandleMovement();

        // Camera zoom in zoom out
        HandleScrollWheel();
    }

    // Process Key Event
    private void Update()
    {
        if (!KeyInteractable)
            return;

        // Camera and Movement Mode
        if (Input.GetKeyDown(SetCameraAndMovementMode))
        {
            HandleSetCameraAndMovementModeKeyDown();
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
    }

    private void HandleMovement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isSprinting = Input.GetKey(SprintKeyCode) && moveInput != Vector2.zero;

        // trigger PlayerMove event
        InputEventManager.TriggerPlayerMove(moveInput);
        // trigger PlayerSprint event
        InputEventManager.TriggerPlayerSprint(isSprinting);
    }

    private void HandleScrollWheel()
    {
        float zoomOffset = Input.GetAxis("Mouse ScrollWheel");

        // trigger Camera Zoom Event
        InputEventManager.TriggerPlayerCameraZoom(zoomOffset);
        // trigger Wave Band
        InputEventManager.TriggerLaserBeamWaveBand(zoomOffset);
    }

    private void HandleSetCameraAndMovementModeKeyDown()
    {
        if (CameraManager.Instance.CameraAiming == true)
            return;

        StartCoroutine(Utility.InvokeBeforeAndAfterSecondes(2.0f, () => { KeyInteractable = false; }, () => { KeyInteractable = true; }));

        // trigger ChangePlayerCameraMode event
        InputEventManager.TriggerChangePlayerCameraMode();
        // trigger ChangePlayerMoveMode event
        InputEventManager.TriggerChangePlayerMoveAndShotMode();
    }

    private void HandleJumpKeyDown()
    {
        // trigger PlayerJump Event
        InputEventManager.TriggerPlayerJump();
    }

    private void HandleFireKeyDown()
    {
        // trigger PlayerBeginShot event
        InputEventManager.TriggerPlayerBeginShot();
    }

    private void HandleFireKeyUp()
    {
        // trigger PlayerEndShot event
        InputEventManager.TriggerPlayerEndShot();
    }

    private void HandleAimKeyDown()
    {
        // trigger PlayerCameraAim event
        InputEventManager.TriggerPlayerCameraAim();
    }
}