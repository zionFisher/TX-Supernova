using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings1 : MonoBehaviour
{
    public Ring[] Rings;

    public GameObject NextRings;

    public float Seconds;

    private void Update()
    {
        bool flag = true;
        foreach (var ring in Rings)
        {
            if (ring.IsLighted != true)
            {
                flag = false;
                break;
            }
        }

        if (flag)
        {
            StartCoroutine("SetFalse");
            enabled = false;
        }
    }

    private IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(Seconds);
        foreach (var ring in Rings)
        {
            ring.gameObject.SetActive(false);
        }
        if (NextRings != null)
        {
            NextRings.SetActive(true);
        }
    }
}