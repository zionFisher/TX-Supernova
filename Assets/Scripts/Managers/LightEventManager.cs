using System;
using UnityEngine;

public static class LightEventManager
{
    public static Action<GameObject> EventLightHitObject;
    public static void TriggerLightHitObject(GameObject hitObject) { if (EventLightHitObject == null) return; EventLightHitObject(hitObject); }
}