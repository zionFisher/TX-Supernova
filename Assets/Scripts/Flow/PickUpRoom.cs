using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRoom : MonoBehaviour
{
    public LightDisk Disk;

    public GameObject WallBefore;

    public GameObject WallAfter;

    public GameObject Mirror;

    private void Update()
    {
        if (Disk.IsLighted == true)
        {
            WallBefore.SetActive(false);
            WallAfter.SetActive(true);
            enabled = false;
            Mirror.SetActive(false);
        }    
    }
}