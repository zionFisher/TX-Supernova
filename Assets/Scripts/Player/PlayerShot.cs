using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public LineRenderer LightCreator;
    public Transform ThirdRDLightLauncher;
    public Transform TwoDotFiveDLightLauncher;

    public ShotMode PlayerShotMode
    {
        get => _playerShotMode;
        set
        {
            if (value == _playerShotMode)
                return;

            _playerShotMode = value;
            UpdateLauncherTransform();
        }
    }

    private ShotMode _playerShotMode = ShotMode.ThirdRDPerson;
    private Transform LightLauncher;
    private int layerMask;

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Raycast Target");
        LightLauncher = ThirdRDLightLauncher;
    }

    private void FixedUpdate()
    {
        UpdateLaserBeam();
    }

    public void ChangeShotMode()
    {
        if (PlayerShotMode == ShotMode.ThirdRDPerson)
            PlayerShotMode = ShotMode.TwoDotFiveD;
        else if (PlayerShotMode == ShotMode.TwoDotFiveD)
            PlayerShotMode = ShotMode.ThirdRDPerson;
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
        CastFirstLaserBeam();
    }

    private void UpdateLauncherTransform()
    {
        if (PlayerShotMode == ShotMode.ThirdRDPerson)
            LightLauncher = ThirdRDLightLauncher;
        else if (PlayerShotMode == ShotMode.TwoDotFiveD)
            LightLauncher = TwoDotFiveDLightLauncher;
    }

    private void CastFirstLaserBeam()
    {
        if (LightCreator.positionCount <= 1)
            return;

        // cast first laser beam
        if (PlayerShotMode == ShotMode.ThirdRDPerson)
            CastFirstLaserBeamThirdRDPerson();
        else if (PlayerShotMode == ShotMode.TwoDotFiveD)
            CastFirstLaserBeamTwoDotFiveD();
    }

    private void CastFirstLaserBeamThirdRDPerson()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask))
        {
            LightCreator.SetPosition(1, hit.point);
            CastLaserBeam(hit, 2);
        }
    }

    private void CastFirstLaserBeamTwoDotFiveD()
    {
        Vector3 launchPosition = LightLauncher.position;

        Ray detect = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(detect, out RaycastHit detectHit, 1000, layerMask))
        {
            Vector3 detectPosition = detectHit.point;
            Vector3 endPosition = new Vector3(detectPosition.x, launchPosition.y, detectPosition.z);

            Vector3 launchDirection = endPosition - launchPosition;

            Ray ray = new Ray(launchPosition, launchDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask))
            {
                LightCreator.SetPosition(1, hit.point);
                CastLaserBeam(hit, 2);
            }
        }
    }

    private void CastLaserBeam(RaycastHit hitInfo, int positionIndex)
    {
        if (positionIndex >= LightCreator.positionCount)
            return;

        // reflect mode only currently
        // TODO: add refract mode
        Vector3 inVec = hitInfo.point - LightLauncher.position;
        Vector3 reflectVec = Vector3.Reflect(inVec, hitInfo.normal);

        Ray ray = new Ray(hitInfo.point, reflectVec);
        if (Physics.Raycast(ray, out RaycastHit reflectHit, 1000, layerMask))
            LightCreator.SetPosition(positionIndex, reflectHit.point);
        else
            LightCreator.SetPosition(positionIndex, LightCreator.GetPosition(positionIndex - 1));

        CastLaserBeam(reflectHit, positionIndex + 1);
    }
}