using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public bool ThirdRDPersonCameraMode = true;
    public CinemachineVirtualCamera ThirdRDPersonCamera;
    public CinemachineVirtualCamera TwoDotFiveDCamera;

    private void Start()
    {
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(ThirdRDPersonCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(TwoDotFiveDCamera);
    }

    private void Update()
    {
        
    }
}