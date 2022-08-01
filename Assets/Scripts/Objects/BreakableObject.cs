using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject BrokenInstance;

    public float DelayTime = 1f;

    private float totalTime = 0f;

    private void Start()
    {
        Utility.CheckUnassignedVar<GameObject>(BrokenInstance);
        enabled = false;
    }

    private void Update()
    {
        if (DelayTime < 0f)
        {
            return;
        }

        if (totalTime >= DelayTime)
        {
            Break();
            enabled = false;
            return;
        }

        totalTime += Time.deltaTime;
    }

    public void BreakObject()
    {
        enabled = true;
    }

    private void Break()
    {
        BrokenInstance.SetActive(true);
        gameObject.SetActive(false);
    }
}