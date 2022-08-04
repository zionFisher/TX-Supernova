using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperationLogic : LogicBase
{
    private PlayerOperationCtrl ctrl;

    public PlayerOperationLogic() : base(ZUIName.PlayerOperationName)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        ctrl = Ctrl as PlayerOperationCtrl;

        UpdatePlayerOperation(CameraManager.Instance.PlayerCameraMode);
        CameraManager.Instance.EventPlayerCameraUpdate += UpdatePlayerOperation;
        GameEventManager.Instance.EventEnableGameInfo += UpdatePlayerOperation;
    }

    protected override void Dispose()
    {
        // CameraManager.Instance.EventPlayerCameraUpdate -= UpdatePlayerOperation;
        // GameEventManager.Instance.EventEnableGameInfo -= UpdatePlayerOperation;
        base.Dispose();
    }

    private void UpdatePlayerOperation(bool enable)
    {
        ctrl.gameObject.SetActive(!enable);
    }

    private void UpdatePlayerOperation(CameraMode mode)
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