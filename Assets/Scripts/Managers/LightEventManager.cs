using System;
using UnityEngine;

public class LightEventManager : Singleton<LightEventManager>
{
    public Action<GameObject> EventLightHitObject;
    public void TriggerLightHitObject(GameObject hitObject) { if (EventLightHitObject == null) return; EventLightHitObject(hitObject); }
}