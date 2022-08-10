using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractRoom : MonoBehaviour
{
    public float Seconds;

    public LightDisk Disk;

    public GameObject RefractGlass;

    public GameObject Wall;

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
        Wall.SetActive(false);
        RefractGlass.SetActive(false);
        yield break;
    }
}