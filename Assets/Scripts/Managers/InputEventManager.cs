using System;
using UnityEngine;

public class InputEventManager : Singleton<InputEventManager>
{
    public Action<Vector2> EventPlayerMove;
    public void TriggerPlayerMove(Vector2 moveInput) { if (EventPlayerMove == null) return; EventPlayerMove(moveInput); }

    public Action<bool> EventPlayerSprint;
    public void TriggerPlayerSprint(bool isSprinting) { if (EventPlayerSprint == null) return; EventPlayerSprint(isSprinting); }

    public Action EventPlayerJump;
    public void TriggerPlayerJump() { if (EventPlayerJump == null) return; EventPlayerJump(); }

    public Action EventChangePlayerCameraMode;
    public void TriggerChangePlayerCameraMode() { if (EventChangePlayerCameraMode == null) return; EventChangePlayerCameraMode(); }

    public Action EventPlayerCameraAim;
    public void TriggerPlayerCameraAim() { if (EventPlayerCameraAim == null) return; EventPlayerCameraAim(); }

    public Action<float> EventPlayerCameraZoom;
    public void TriggerPlayerCameraZoom(float zoomOffset) { if (EventPlayerCameraZoom == null) return; EventPlayerCameraZoom(zoomOffset); }

    public Action<float> EventLaserBeamWaveBand;
    public void TriggerLaserBeamWaveBand(float bandOffset) { if (EventLaserBeamWaveBand == null) return; EventLaserBeamWaveBand(bandOffset); }

    public Action EventChangePlayerMoveAndShotMode;
    public void TriggerChangePlayerMoveAndShotMode() { if (EventChangePlayerMoveAndShotMode == null) return; EventChangePlayerMoveAndShotMode(); }

    public Action EventPlayerBeginShot;
    public void TriggerPlayerBeginShot() { if (EventPlayerBeginShot == null) return; EventPlayerBeginShot(); }

    public Action EventPlayerEndShot;
    public void TriggerPlayerEndShot() { if (EventPlayerEndShot == null) return; EventPlayerEndShot(); }
}