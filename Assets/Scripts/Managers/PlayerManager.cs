using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject PlayerRoot;
    public GameObject PlayerHead;
    public GameObject PlayerMesh;

    [SerializeField] private PlayerAnimation playerAnim;
    [SerializeField] private PlayerMovement playerMove;

    private void Start()
    {
        Utility.CheckUnassignedVar<GameObject>(PlayerRoot);
        Utility.CheckUnassignedVar<GameObject>(PlayerHead);
        Utility.CheckUnassignedVar<GameObject>(PlayerMesh);
    }

    public void SetPlayerMeshActive(bool active)
    {
        PlayerMesh.SetActive(active);
    }

    public void PlayerAim(bool aiming)
    {
        if (CameraManager.Instance.CharacterCameraMode == CameraMode.TwoDotFiveD)
            return;

        if (aiming)
            SetPlayerMeshActive(false);
        else
            SetPlayerMeshActive(true);
    }

    public void PlayerMove(Vector2 moveInput)
    {
        playerMove.Move(moveInput);
    }

    public void PlayerSprint(bool isSprinting)
    {
        playerMove.Sprint(isSprinting);
    }

    public void ChangePlayerMoveMode()
    {
        playerMove.ChangeMoveMode();
    }
}