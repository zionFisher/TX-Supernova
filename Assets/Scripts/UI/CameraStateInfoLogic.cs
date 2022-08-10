using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateInfoLogic : LogicBase
{
    private CameraStateInfoCtrl ctrl;

    public CameraStateInfoLogic() : base(ZUIName.CameraStateInfoName)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        ctrl = Ctrl as CameraStateInfoCtrl;

        UpdateCameraStateInfo(CameraManager.Instance.PlayerCameraMode);
        CameraManager.Instance.EventPlayerCameraUpdate += UpdateCameraStateInfo;
        GameEventManager.Instance.EventEnableGameInfo += UpdateCameraStateInfo;
    }

    protected override void Dispose()
    {
        // CameraManager.Instance.EventPlayerCameraUpdate -= UpdateCameraStateInfo;
        // GameEventManager.Instance.EventEnableGameInfo -= UpdateCameraStateInfo;
        base.Dispose();
    }

    private void UpdateCameraStateInfo(bool enable)
    {
        ctrl.gameObject.SetActive(!enable);
    }

    private void UpdateCameraStateInfo(CameraMode mode)
    {
        ctrl.ThirdRDPersonView.SetActive(false);
        ctrl.ThirdRDPersonShotView.SetActive(false);
        ctrl.TwoDotFiveDView.SetActive(false);
        ctrl.TwoDotFiveDShotView.SetActive(false);

        switch (mode)
        {
            case CameraMode.ThirdRDPerson:
            {
                ctrl.ThirdRDPersonView.SetActive(true);
                break;
            }
            case CameraMode.FPS3D:
            {
                ctrl.ThirdRDPersonShotView.SetActive(true);
                break;
            }
            case CameraMode.TwoDotFiveD:
            {
                ctrl.TwoDotFiveDView.SetActive(true);
                break;
            }
            case CameraMode.FPS2Dot5D:
            {
                ctrl.TwoDotFiveDShotView.SetActive(true);
                break;
            }
            default:
            {
                Debug.LogError("Uncompleted.");
                break;
            }
        }
    }
}