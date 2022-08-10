using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings7 : MonoBehaviour
{
    public Ring[] Rings;

    public GameObject NextRings;

    public GameObject Mirror;

    public GameObject[] Others;

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
        if (Mirror != null)
        {
            for (int i = 0; i < Mirror.transform.childCount; i++)
            {
                Mirror.transform.GetChild(i).tag = "Untagged";
            }
        }
        foreach (var it in Others)
        {
            it.gameObject.SetActive(false);
        }
    }
}