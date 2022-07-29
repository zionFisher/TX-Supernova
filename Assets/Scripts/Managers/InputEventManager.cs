using System;
using UnityEngine;

public static class InputEventManager
{
    public static Action<Vector2> EventPlayerMove;
    public static void TriggerPlayerMove(Vector2 moveInput) { if (EventPlayerMove == null) return; EventPlayerMove(moveInput); }

    public static Action<bool> EventPlayerSprint;
    public static void TriggerPlayerSprint(bool isSprinting) { if (EventPlayerSprint == null) return; EventPlayerSprint(isSprinting); }

    public static Action EventPlayerJump;
    public static void TriggerPlayerJump() { if (EventPlayerJump == null) return; EventPlayerJump(); }

    public static Action EventChangePlayerCameraMode;
    public static void TriggerChangePlayerCameraMode() { if (EventChangePlayerCameraMode == null) return; EventChangePlayerCameraMode(); }

    public static Action EventPlayerCameraAim;
    public static void TriggerPlayerCameraAim() { if (EventPlayerCameraAim == null) return; EventPlayerCameraAim(); }

    public static Action<float> EventPlayerCameraZoom;
    public static void TriggerPlayerCameraZoom(float zoomOffset) { if (EventPlayerCameraZoom == null) return; EventPlayerCameraZoom(zoomOffset); }

    public static Action EventChangePlayerMoveAndShotMode;
    public static void TriggerChangePlayerMoveAndShotMode() { if (EventChangePlayerMoveAndShotMode == null) return; EventChangePlayerMoveAndShotMode(); }

    public static Action EventPlayerBeginShot;
    public static void TriggerPlayerBeginShot() { if (EventPlayerBeginShot == null) return; EventPlayerBeginShot(); }

    public static Action EventPlayerEndShot;
    public static void TriggerPlayerEndShot() { if (EventPlayerEndShot == null) return; EventPlayerEndShot(); }
}