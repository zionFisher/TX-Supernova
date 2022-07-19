using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraMode
{
    ThirdRDPerson, TwoDotFiveD, FPS, DollyTrack
}

public class CameraManager : Singleton<CameraManager>
{
    public CameraMode CharacterCameraMode = CameraMode.ThirdRDPerson;
    public CinemachineVirtualCamera ThirdRDPersonCamera;
    public CinemachineVirtualCamera TwoDotFiveDCamera;
    public CinemachineVirtualCamera FPSCamera;

    private int characterCameraPriority = 10;

    private void Start()
    {
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(ThirdRDPersonCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(TwoDotFiveDCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(FPSCamera);
    }

    private void Update()
    {
        
    }

    public void ChangeCharacterCameraMode()
    {
        if (CharacterCameraMode == CameraMode.ThirdRDPerson)
            CharacterCameraMode = CameraMode.TwoDotFiveD;
        else if (CharacterCameraMode == CameraMode.TwoDotFiveD)
            CharacterCameraMode = CameraMode.ThirdRDPerson;
        
        AdjustCharacterCameraMode();
    }

    public void EnableFPSCamera()
    {
        FPSCamera.Priority = characterCameraPriority + 3;
    }

    public void DisableFPSCamera()
    {
        FPSCamera.Priority = characterCameraPriority - 3;
    }

    private void AdjustCharacterCameraMode()
    {
        if (CharacterCameraMode == CameraMode.ThirdRDPerson)
        {
            ThirdRDPersonCamera.Priority = characterCameraPriority + 1;
            TwoDotFiveDCamera.Priority = characterCameraPriority - 1;
        }
        else if (CharacterCameraMode == CameraMode.TwoDotFiveD)
        {
            ThirdRDPersonCamera.Priority = characterCameraPriority - 1;
            TwoDotFiveDCamera.Priority = characterCameraPriority + 1;
        }
    }
}