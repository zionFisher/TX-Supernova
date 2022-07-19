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
    public CameraMode CharacterCameraMode
    {
        get => _characterCameraMode;
        set
        {
            if (value == _characterCameraMode)
                return;

            _characterCameraMode = value;
            UpdateCameraPriority(_characterCameraMode);
        }
    }

    private CameraMode _characterCameraMode;

    public CinemachineVirtualCamera ThirdRDPersonCamera;
    public CinemachineVirtualCamera TwoDotFiveDCamera;
    public CinemachineVirtualCamera FPSCamera;

    private int characterCameraStandardPriority = 10;

    private void Start()
    {
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(ThirdRDPersonCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(TwoDotFiveDCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(FPSCamera);
        CharacterCameraMode = CameraMode.ThirdRDPerson;
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
    }

    public void HandleCameraAim(bool aiming)
    {
        if (aiming)
        {
            if (CharacterCameraMode == CameraMode.ThirdRDPerson)
                CharacterCameraMode = CameraMode.FPS;
        }
        else
        {
            if (CharacterCameraMode == CameraMode.FPS)
                CharacterCameraMode = CameraMode.ThirdRDPerson;
        }
    }

    private void UpdateCameraPriority(CameraMode mode)
    {
        // reset priority
        ThirdRDPersonCamera.Priority = characterCameraStandardPriority;
        TwoDotFiveDCamera.Priority = characterCameraStandardPriority;
        FPSCamera.Priority = characterCameraStandardPriority;

        // set priority
        switch (mode)
        {
            case CameraMode.ThirdRDPerson:
            {
                ThirdRDPersonCamera.Priority += 2;
                break;
            }
            case CameraMode.TwoDotFiveD:
            {
                TwoDotFiveDCamera.Priority += 2;
                break;
            }
            case CameraMode.FPS:
            {
                FPSCamera.Priority += 2;
                break;
            }
            default:
            {
                Debug.LogError("No such camera mode, please check input.");
                break;
            };
        }
    }
}