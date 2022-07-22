using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    public bool CameraAiming
    {
        get => _cameraAiming;
        set
        {
            if (value == _cameraAiming)
                return;

            _cameraAiming = value;
            UpdateCameraAiming(_cameraAiming);
        }
    }

    private CameraMode _characterCameraMode;
    private bool _cameraAiming;
    private int characterCameraStandardPriority = 10;

    public CinemachineVirtualCamera ThirdRDPersonCamera;
    public CinemachineVirtualCamera TwoDotFiveDCamera;
    public CinemachineVirtualCamera FPSCamera;

    private void Start()
    {
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(ThirdRDPersonCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(TwoDotFiveDCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(FPSCamera);
        CharacterCameraMode = CameraMode.ThirdRDPerson;
    }

    public void ChangeCharacterCameraMode()
    {
        if (CharacterCameraMode == CameraMode.ThirdRDPerson)
            CharacterCameraMode = CameraMode.TwoDotFiveD;
        else if (CharacterCameraMode == CameraMode.TwoDotFiveD)
            CharacterCameraMode = CameraMode.ThirdRDPerson;
    }

    public void CameraAim()
    {
        CameraAiming = !CameraAiming;
    }

    private void UpdateCameraAiming(bool aiming)
    {
        if (aiming)
        {
            if (CharacterCameraMode == CameraMode.ThirdRDPerson)
                CharacterCameraMode = CameraMode.FPS3D;
            if (CharacterCameraMode == CameraMode.TwoDotFiveD)
                CharacterCameraMode = CameraMode.FPS2Dot5D;
        }
        else
        {
            if (CharacterCameraMode == CameraMode.FPS3D)
                CharacterCameraMode = CameraMode.ThirdRDPerson;
            if (CharacterCameraMode == CameraMode.FPS2Dot5D)
                CharacterCameraMode = CameraMode.TwoDotFiveD;
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
            case CameraMode.FPS3D:
            {
                FPSCamera.Priority += 2;
                break;
            }
            case CameraMode.FPS2Dot5D:
            {
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