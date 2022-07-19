using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementMode
{
    ThirdRDPerson, TwoDotFiveD
}

public class CharacterMove : MonoBehaviour
{
    public MovementMode CharacterMovementMode = MovementMode.ThirdRDPerson;
    public float TurnSpeed = 10f;
    public KeyCode SprintKeyCode = KeyCode.LeftShift;
    public KeyCode JumpKeyCode = KeyCode.Space;

    private Animator anim;
    private Camera mainCamera;

    private float speed = 0f;
    private float smoothVelocity = 0f;
    private bool isSprinting = false;

    private Vector3 localForward;
    private Vector2 moveInput;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        Utility.CheckUnassignedVar<Animator>(anim);
        Utility.CheckUnassignedVar<Camera>(mainCamera);
    }

    public void ChangeMoveMode()
    {
        if (CharacterMovementMode == MovementMode.ThirdRDPerson)
            CharacterMovementMode = MovementMode.TwoDotFiveD;
        else if (CharacterMovementMode == MovementMode.TwoDotFiveD)
            CharacterMovementMode = MovementMode.ThirdRDPerson;
    }

    public void Move(Vector2 moveInput)
    {
        this.moveInput = moveInput;

        if (CharacterMovementMode == MovementMode.ThirdRDPerson)
            ThirdRDPersonMove();
        else if (CharacterMovementMode == MovementMode.TwoDotFiveD)
            TwoDotFiveDMove();
    }

    public void Sprint(bool isSprinting)
    {
        anim.SetBool("isSprinting", isSprinting);
    }

    private void ThirdRDPersonMove()
    {
        speed = Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y);
        speed = Mathf.Clamp(speed, 0f, 1f);
        speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref smoothVelocity, 0.1f); // smooth speed
        anim.SetFloat("Speed", speed);
        UpdateOrientation();
    }

    private void TwoDotFiveDMove()
    {

    }

    private void UpdateOrientation()
    {
        var right = mainCamera.transform.TransformDirection(Vector3.right);
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);

        forward.y = 0;
        localForward = moveInput.x * right + moveInput.y * forward;

        if (moveInput != Vector2.zero && localForward.magnitude > 0.1f)
        {
            Vector3 lookDirection = localForward.normalized;
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation != 0)
                eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), TurnSpeed * Time.deltaTime);
        }
    }
}