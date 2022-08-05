using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectRoom : MonoBehaviour
{
    public float Seconds;

    public LightDisk Disk;

    public GameObject WallBefore;

    public GameObject WallAfter;

    private void Update()
    {
        if (Disk.IsLighted == true)
        {
            StartCoroutine("Action");
            enabled = false;
        }    
    }

    private IEnumerator Action()
    {
        yield return new WaitForSeconds(Seconds);
        WallBefore.SetActive(false);
        WallAfter.SetActive(true);
        yield break;
    }
}