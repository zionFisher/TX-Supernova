using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1DefaultUILoader : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.LoadUI(typeof(CameraStateInfoLogic));
    }
}