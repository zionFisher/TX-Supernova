using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject PlayerRoot;
    public GameObject PlayerHead;
    public GameObject PlayerMesh;

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

    public void HandlePlayerAim(bool aiming)
    {
        if (CameraManager.Instance.CharacterCameraMode == CameraMode.TwoDotFiveD)
            return;

        if (aiming)
            SetPlayerMeshActive(false);
        else
            SetPlayerMeshActive(true);
    }
}