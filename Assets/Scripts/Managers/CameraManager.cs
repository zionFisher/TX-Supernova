using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraMode
{
    ThirdRDPerson, TwoDotFiveD, DollyTrack
}

public class CameraManager : Singleton<CameraManager>
{
    public CameraMode CharacterCameraMode = CameraMode.ThirdRDPerson;
    public CinemachineVirtualCamera ThirdRDPersonCamera;
    public CinemachineVirtualCamera TwoDotFiveDCamera;

    private int characterCameraPriority = 10;

    private void Start()
    {
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(ThirdRDPersonCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(TwoDotFiveDCamera);
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