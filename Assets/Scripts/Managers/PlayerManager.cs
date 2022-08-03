using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject PlayerRoot;
    public GameObject PlayerHead;
    public GameObject PlayerMesh;
    public GameObject PlayerGun;

    public int MaxTime = 2;

    [SerializeField] private PlayerAnimation playerAnim;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private PlayerShot playerShot;

    private void Start()
    {
        Utility.CheckUnassignedVar<GameObject>(PlayerRoot);
        Utility.CheckUnassignedVar<GameObject>(PlayerHead);
        Utility.CheckUnassignedVar<GameObject>(PlayerMesh);

        InputEventManager.EventPlayerMove += PlayerMove;
        InputEventManager.EventPlayerSprint += PlayerSprint;
        InputEventManager.EventChangePlayerMoveAndShotMode += ChangePlayerMoveAndShotMode;
        InputEventManager.EventPlayerBeginShot += PlayerShot;
        InputEventManager.EventPlayerEndShot += PlayerStopShot;
    }

    protected override void OnDestroy()
    {
        InputEventManager.EventPlayerMove -= PlayerMove;
        InputEventManager.EventPlayerSprint -= PlayerSprint;
        InputEventManager.EventChangePlayerMoveAndShotMode -= ChangePlayerMoveAndShotMode;
        InputEventManager.EventPlayerBeginShot -= PlayerShot;
        InputEventManager.EventPlayerEndShot -= PlayerStopShot;

        base.OnDestroy();
    }

    public void SetPlayerMeshActive(bool active)
    {
        PlayerMesh.SetActive(active);
        PlayerGun.SetActive(active);
    }

    public void PlayerAim(bool aiming)
    {
        if (CameraManager.Instance.PlayerCameraMode == CameraMode.TwoDotFiveD || CameraManager.Instance.PlayerCameraMode == CameraMode.FPS2Dot5D)
            return;

        if (CameraManager.Instance.CameraAiming == true)
            SetPlayerMeshActive(false);
        else
            SetPlayerMeshActive(true);
    }

    public void PlayerMove(Vector2 moveInput)
    {
        playerMove.Move(moveInput);
    }

    public void PlayerStopMove()
    {
        playerMove.UpdateSpeed(0f);
        playerAnim.SetSprint(false);
    }

    public void PlayerSprint(bool isSprinting)
    {
        playerMove.Sprint(isSprinting);
    }

    public void PlayerShot()
    {
        if (CameraManager.Instance.PlayerCameraMode != CameraMode.FPS3D && CameraManager.Instance.PlayerCameraMode != CameraMode.FPS2Dot5D)
            return;

        playerShot.Shot(MaxTime);
    }

    public void PlayerStopShot()
    {
        playerShot.Clear();
    }

    public void ChangePlayerMoveAndShotMode()
    {
        if (CameraManager.Instance.CameraAiming == true)
            return;

        playerMove.ChangeMoveMode();
        playerShot.ChangeShotMode();
    }
}