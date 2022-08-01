using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : Singleton<LightManager>
{
    private void Start()
    {
        LightEventManager.EventLightHitObject += LightHitObject;
    }

    protected override void OnDestroy()
    {
        LightEventManager.EventLightHitObject -= LightHitObject;
        base.OnDestroy();
    }

    private void LightHitObject(GameObject hitObject)
    {
        if (hitObject.tag == "Breakable")
            LightHitBreakableObject(hitObject);   
    }

    private void LightHitBreakableObject(GameObject hitObject)
    {
        hitObject.GetComponent<BreakableObject>().BreakObject();
    }
}