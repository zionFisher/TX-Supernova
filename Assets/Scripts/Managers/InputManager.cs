using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool Interactable = true;
    public KeyCode SetCameraAndMovementMode = KeyCode.V;
    public KeyCode SprintKeyCode = KeyCode.LeftShift;
    public KeyCode JumpKeyCode = KeyCode.Space;
    public KeyCode FireKeyCode = KeyCode.Mouse0;
    public CharacterMove PlayerMove;

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
    }

    private void FixedUpdate()
    {
        if (!Interactable)
            return;

        // Movement
        HandleMovement();

        // Camera and Movement Mode
        if (Input.GetKey(SetCameraAndMovementMode))
        {
            HandleCameraMode();
            HandleMovementMode();
        }
    }

    private void HandleMovement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        PlayerMove.Move(moveInput);
        bool isSprinting = Input.GetKey(SprintKeyCode) && moveInput != Vector2.zero;
        HandleSprint(isSprinting);
    }

    private void HandleSprint(bool isSprinting)
    {
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
}