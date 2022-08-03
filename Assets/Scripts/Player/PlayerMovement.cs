using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMovement : MonoBehaviour
{
    public MovementMode PlayerMovementMode = MovementMode.ThirdRDPerson;
    public float TurnSpeed = 10f;

    private PlayerAnimation playerAnimation;
    private Vector3 localForward;
    private Vector2 moveInput;
    private float speed = 0f;
    private float smoothVelocity = 0f;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void ChangeMoveMode()
    {
        if (PlayerMovementMode == MovementMode.ThirdRDPerson)
            PlayerMovementMode = MovementMode.TwoDotFiveD;
        else if (PlayerMovementMode == MovementMode.TwoDotFiveD)
            PlayerMovementMode = MovementMode.ThirdRDPerson;
    }

    public void Move(Vector2 moveInput)
    {
        this.moveInput = moveInput;

        if (PlayerMovementMode == MovementMode.ThirdRDPerson)
            ThirdRDPersonMove();
        else if (PlayerMovementMode == MovementMode.TwoDotFiveD)
            TwoDotFiveDMove();
    }

    public void Sprint(bool isSprinting)
    {
        playerAnimation.SetSprint(isSprinting);
    }

    private void ThirdRDPersonMove()
    {
        UpdateSpeed();
        UpdateThirdRDPersonForward();
    }

    private void TwoDotFiveDMove()
    {
        UpdateSpeed();
        UpdateTwoDotFiveDForward();
    }

    public void UpdateSpeed(float speed)
    {
        this.speed = speed;
        playerAnimation.SetSpeed(speed);
    }

    private void UpdateSpeed()
    {
        speed = Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y);
        speed = Mathf.Clamp(speed, 0f, 1f);
        speed = Mathf.SmoothDamp(playerAnimation.GetSpeed(), speed, ref smoothVelocity, 0.1f); // smooth speed
        playerAnimation.SetSpeed(speed);
    }

    private void UpdateThirdRDPersonForward()
    {
        Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

        forward.y = 0;
        localForward = moveInput.x * right + moveInput.y * forward;
        UpdateOrientation();
    }

    private void UpdateTwoDotFiveDForward()
    {
        Vector3 right = Vector3.right;
        Vector3 forward = Vector3.forward;

        localForward = moveInput.x * right + moveInput.y * forward;
        UpdateOrientation();
    }

    private void UpdateOrientation()
    {
        if (moveInput != Vector2.zero && localForward.magnitude > 0.1f)
        {
            Vector3 lookDirection = localForward.normalized;
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            float diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            float eulerY = transform.eulerAngles.y;

            if (diferenceRotation != 0)
                eulerY = freeRotation.eulerAngles.y;
            Vector3 euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), TurnSpeed * Time.deltaTime);
        }
    }
}