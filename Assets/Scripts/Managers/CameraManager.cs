using System;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera ThirdRDPersonCamera;
    public float ThirdRDPersonCameraZoomSpeed = 3f;
    public float MinThirdRDPersonCameraDistance = 2f;
    public float MaxThirdRDPersonCameraDistance = 5f;
    public CinemachineVirtualCamera TwoDotFiveDCamera;
    public float TwoDotFiveDCameraZoomSpeed = 5f;
    public float MinTwoDotFiveDCameraDistance = 6f;
    public float MaxTwoDotFiveDCameraDistance = 15f;
    public CinemachineVirtualCamera FPSCamera;

    public Action<CameraMode> EventPlayerCameraUpdate;
    public void TriggerPlayerCameraUpdate(CameraMode mode) { if (EventPlayerCameraUpdate == null) return; EventPlayerCameraUpdate(mode); }

    public CameraMode PlayerCameraMode
    {
        get => _characterCameraMode;
        set
        {
            if (value == _characterCameraMode)
                return;

            _characterCameraMode = value;
            UpdateCameraPriority(_characterCameraMode);
            TriggerPlayerCameraUpdate(value);
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

    public float CameraThirdRDPersonDDistance
    {
        get => _cameraThirdRDPersonDDistance;
    }

    public float CameraTwoDotFiveDDistance
    {
        get => _cameraTwoDotFiveDDistance;
        set
        {
            if (value == _cameraTwoDotFiveDDistance)
                return;

            _cameraTwoDotFiveDDistance = value;
            UpdateCameraDistance(_cameraTwoDotFiveDDistance);
        }
    }

    private CameraMode _characterCameraMode;
    private bool _cameraAiming = false;
    private float _cameraThirdRDPersonDDistance = 2f;
    private float _cameraTwoDotFiveDDistance = 6f;
    private int characterCameraStandardPriority = 10;
    private CinemachineFramingTransposer twoDotFiveDFramingTransposer;

    private void Start()
    {
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(ThirdRDPersonCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(TwoDotFiveDCamera);
        Utility.CheckUnassignedVar<CinemachineVirtualCamera>(FPSCamera);
        PlayerCameraMode = CameraMode.ThirdRDPerson;
        twoDotFiveDFramingTransposer = TwoDotFiveDCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        twoDotFiveDFramingTransposer.m_CameraDistance = CameraTwoDotFiveDDistance;

        InputEventManager.EventChangePlayerCameraMode += ChangePlayerCameraMode;
        InputEventManager.EventPlayerCameraAim += PlayerCameraAim;
        InputEventManager.EventPlayerCameraZoom += PlayerCameraZoom;
    }

    protected override void OnDestroy()
    {
        InputEventManager.EventChangePlayerCameraMode -= ChangePlayerCameraMode;
        InputEventManager.EventPlayerCameraAim -= PlayerCameraAim;
        InputEventManager.EventPlayerCameraZoom -= PlayerCameraZoom;

        base.OnDestroy();
    }

    public void ChangePlayerCameraMode()
    {
        if (CameraAiming == true)
            return;

        if (PlayerCameraMode == CameraMode.ThirdRDPerson)
            PlayerCameraMode = CameraMode.TwoDotFiveD;
        else if (PlayerCameraMode == CameraMode.TwoDotFiveD)
            PlayerCameraMode = CameraMode.ThirdRDPerson;
    }

    public void PlayerCameraAim()
    {
        CameraAiming = !CameraAiming;
        PlayerManager.Instance.PlayerAim(CameraAiming);
    }

    public void PlayerCameraZoom(float zoomOffset)
    {
        if (PlayerCameraMode == CameraMode.ThirdRDPerson)
            ThirdRDPersonPlayerCameraZoom(zoomOffset);
        else if (PlayerCameraMode == CameraMode.TwoDotFiveD)
            TwoDotFiveDPlayerCameraZoom(zoomOffset);
    }

    private void ThirdRDPersonPlayerCameraZoom(float zoomOffset)
    {

    }

    private void TwoDotFiveDPlayerCameraZoom(float zoomOffset)
    {
        float distance = CameraTwoDotFiveDDistance - zoomOffset * TwoDotFiveDCameraZoomSpeed;
        distance = Mathf.Clamp(distance, MinTwoDotFiveDCameraDistance, MaxTwoDotFiveDCameraDistance);

        CameraTwoDotFiveDDistance = distance;
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

    private void UpdateCameraDistance(float cameraDistance)
    {
        twoDotFiveDFramingTransposer.m_CameraDistance = cameraDistance;
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