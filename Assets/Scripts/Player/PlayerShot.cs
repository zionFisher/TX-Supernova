using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public LineRenderer LightCreator;
    public Transform LightLauncher;

    private void Update()
    {
        LightCreator.SetPosition(0, LightLauncher.position);
        if (LightCreator.positionCount == 3)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                LightCreator.SetPosition(1, hit.point);

                Vector3 inVec = hit.point - LightLauncher.position;
                Vector3 reflectVec = Vector3.Reflect(inVec, hit.normal);

                ray = new Ray(hit.point, reflectVec);
                Physics.Raycast(ray, out RaycastHit hit2);

                LightCreator.SetPosition(2, hit2.point);
            }
        }
    }

    public void Shot()
    {
        LightCreator.positionCount = 3;
    }

    public void Clear()
    {
        LightCreator.positionCount = 1;
    }
}