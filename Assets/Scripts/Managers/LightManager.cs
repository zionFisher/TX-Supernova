using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : Singleton<LightManager>
{
    public LaserBeamController LBC;

    private void Start()
    {
        LightEventManager.Instance.EventLightHitObject += LightHitObject;
    }

    protected override void OnDestroy()
    {
        // LightEventManager.Instance.EventLightHitObject -= LightHitObject;
        base.OnDestroy();
    }

    private void LightHitObject(GameObject hitObject)
    {
        if (hitObject.tag == "Breakable")
            LightHitBreakableObject(hitObject, LBC.curFrequency);
        if (hitObject.tag == "Light Disk")
            LightHitLightDisk(hitObject);
    }

    private void LightHitBreakableObject(GameObject hitObject, float frequency)
    {
        hitObject.GetComponent<BreakableObject>().BreakObject(frequency);
    }

    private void LightHitLightDisk(GameObject hitObject)
    {
        hitObject.GetComponent<LightDisk>().LightLightDisk();
    }
}