using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float GetSpeed()
    {
        return animator.GetFloat("Speed");
    }

    public bool GetIsSprinting()
    {
        return animator.GetBool("isSprinting");
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void SetSprint(bool isSprinting)
    {
        animator.SetBool("isSprinting", isSprinting);
    }
}
