using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    public bool IsDestroy = true;

    public float DestroyTime = 1f;

    private float totalTime = 0f;

    private void Update()
    {
        if (DestroyTime < 0f)
        {
            return;
        }

        if (totalTime >= DestroyTime)
        {
            Break();
            return;
        }

        totalTime += Time.deltaTime;
    }

    private void Break()
    {
        GameObject.Destroy(gameObject);
    }
}