using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool Interactable = true;
    public CharacterMove PlayerMove;

    public KeyCode SetCameraAndMovementMode = KeyCode.V;
    public KeyCode SprintKeyCode            = KeyCode.LeftShift;
    public KeyCode JumpKeyCode              = KeyCode.Space;
    public KeyCode FireKeyCode              = KeyCode.Mouse0;
    public KeyCode AimKeyCode               = KeyCode.Mouse1;


    private void Awake()
    {

    }

    private void Start()
    {
        Utility.CheckUnassignedVar<CharacterMove>(PlayerMove);
    }

    private void Update()
    {
        if (!Interactable)
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

    private void FixedUpdate()
    {
        if (!Interactable)
            return;

        // Movement
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        PlayerMove.Move(moveInput);
        bool isSprinting = Input.GetKey(SprintKeyCode) && moveInput != Vector2.zero;
        PlayerMove.Sprint(isSprinting);
    }

    private void HandleCameraMode()
    {
        CameraManager.Instance.ChangeCharacterCameraMode();
        StartCoroutine(Utility.InvokeBeforeAndAfterSecondes(1.0f, () => { Interactable = false; }, () => { Interactable = true; }));
    }

    private void HandleMovementMode()
    {
        PlayerMove.ChangeMoveMode();
    }

    private void HandleJumpKey()
    {
        // TODO:
    }

    private void HandleFireKeyDown()
    {
        
    }

    private void HandleFireKeyUp()
    {
        
    }

    private void HandleAimKeyDown()
    {
        CameraManager.Instance.EnableFPSCamera();
        PlayerManager.Instance.SetPlayerMeshActive(false);
    }

    private void HandleAimKeyUp()
    {
        CameraManager.Instance.DisableFPSCamera();
        PlayerManager.Instance.SetPlayerMeshActive(true);
    }
}