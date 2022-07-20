using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public LineRenderer LightCreator;
    public Transform LightLauncher;

    private int layerMask;

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Raycast Target");
    }

    private void FixedUpdate()
    {
        UpdateLaserBeam();
    }

    public void Shot(int maxReflectTime)
    {
        LightCreator.positionCount = maxReflectTime + 2;
    }

    public void Clear()
    {
        LightCreator.positionCount = 1;
    }

    private void UpdateLaserBeam()
    {
        LightCreator.SetPosition(0, LightLauncher.position);
        CastLaserBeam();
    }

    private void CastLaserBeam()
    {
        if (LightCreator.positionCount <= 1)
            return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask))
        {
            LightCreator.SetPosition(1, hit.point);
            ReflectLaserBeam(hit, 2);
        }
    }

    private void ReflectLaserBeam(RaycastHit hitInfo, int positionIndex)
    {
        if (positionIndex >= LightCreator.positionCount)
            return;

        Vector3 inVec = hitInfo.point - LightLauncher.position;
        Vector3 reflectVec = Vector3.Reflect(inVec, hitInfo.normal);

        Ray ray = new Ray(hitInfo.point, reflectVec);
        if (Physics.Raycast(ray, out RaycastHit reflectHit, 1000, layerMask))
            LightCreator.SetPosition(positionIndex, reflectHit.point);
        
        ReflectLaserBeam(reflectHit, positionIndex + 1);
    }
}