using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float TurnSpeed = 10f;
    public KeyCode SprintKeyCode = KeyCode.LeftShift;
    public KeyCode JumpKeyCode = KeyCode.Space;

    private float speed = 0f;
    private bool isSprinting = false;
    private Animator anim;
    private Vector3 targetDirection;
    private Vector2 input;
    private Quaternion freeRotation;
    private Camera mainCamera;
    private float velocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

        speed = Mathf.Clamp(speed, 0f, 1f);
        speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
        anim.SetFloat("Speed", speed);

        isSprinting = ((Input.GetKey(SprintKeyCode)) && input != Vector2.zero);
        anim.SetBool("isSprinting", isSprinting);

        UpdateTargetDirection();
        
    }

    private void UpdateTargetDirection()
    {
        var right = mainCamera.transform.TransformDirection(Vector3.right);
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);

        forward.y = 0;
        targetDirection = input.x * right + input.y * forward;

        if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation != 0)
                eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), TurnSpeed * Time.deltaTime);
        }
    }
}