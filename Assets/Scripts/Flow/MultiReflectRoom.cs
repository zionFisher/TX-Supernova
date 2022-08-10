using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiReflectRoom : MonoBehaviour
{
    public float Seconds;
    public float CloseSeconds;

    public LightDisk Disk;

    public GameObject WallClose1;
    public GameObject WallClose2;
    public GameObject WallOpen1;
    public GameObject WallOpen2;

    private bool isOpen = false;

    private void Update()
    {
        if (Disk.IsLighted == true && isOpen == false)
        {
            StartCoroutine("Action");
        }    
    }

    private IEnumerator Action()
    {
        isOpen = true;
        yield return new WaitForSeconds(Seconds);
        WallClose1.SetActive(false);
        WallClose2.SetActive(false);
        WallOpen1.SetActive(true);
        WallOpen2.SetActive(true);
        enabled = false;
        yield return new WaitForSeconds(CloseSeconds);
        WallClose1.SetActive(true);
        WallClose2.SetActive(true);
        WallOpen1.SetActive(false);
        WallOpen2.SetActive(false);
        isOpen = false;
        enabled = true;
        yield break;
    }
}