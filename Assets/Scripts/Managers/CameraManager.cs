using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public CameraMode PlayerCameraMode
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
        PlayerCameraMode = CameraMode.ThirdRDPerson;

        InputEventManager.EventChangePlayerCameraMode += ChangePlayerCameraMode;
        InputEventManager.EventPlayerCameraAim += PlayerCameraAim;
    }

    protected override void OnDestroy()
    {
        InputEventManager.EventChangePlayerCameraMode -= ChangePlayerCameraMode;
        InputEventManager.EventPlayerCameraAim -= PlayerCameraAim;

        base.OnDestroy();
    }

    public void ChangePlayerCameraMode()
    {
        if (PlayerCameraMode == CameraMode.ThirdRDPerson)
            PlayerCameraMode = CameraMode.TwoDotFiveD;
        else if (PlayerCameraMode == CameraMode.TwoDotFiveD)
            PlayerCameraMode = CameraMode.ThirdRDPerson;
    }

    public void PlayerCameraAim()
    {
        CameraAiming = !CameraAiming;
    }

    private void UpdateCameraAiming(bool aiming)
    {
        if (aiming)
        {
            if (PlayerCameraMode == CameraMode.ThirdRDPerson)
                PlayerCameraMode = CameraMode.FPS3D;
            if (PlayerCameraMode == CameraMode.TwoDotFiveD)
                PlayerCameraMode = CameraMode.FPS2Dot5D;
        }
        else
        {
            if (PlayerCameraMode == CameraMode.FPS3D)
                PlayerCameraMode = CameraMode.ThirdRDPerson;
            if (PlayerCameraMode == CameraMode.FPS2Dot5D)
                PlayerCameraMode = CameraMode.TwoDotFiveD;
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