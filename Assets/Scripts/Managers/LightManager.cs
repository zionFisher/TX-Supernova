using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : Singleton<LightManager>
{
    public LaserBeamController LBC;

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
            LightHitBreakableObject(hitObject, LBC.curFrequency);   
    }

    private void LightHitBreakableObject(GameObject hitObject, float frequency)
    {
        hitObject.GetComponent<BreakableObject>().BreakObject(frequency);
    }
}