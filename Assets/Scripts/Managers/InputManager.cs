using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool Interactable = true;
    public KeyCode SetCameraMode = KeyCode.V;
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

        // Camera Mode
        if (Input.GetKey(SetCameraMode))
        {

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

    private void HandleMoveInput()
    {

    }
}