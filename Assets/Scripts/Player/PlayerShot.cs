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
        layerMask = 1 << LayerMask.NameToLayer("Reflect");
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
        LightCreator.SetPosition(0, LightLauncher.position);
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
        Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            LightCreator.SetPosition(1, hit.point);
            LightEventManager.TriggerLightHitObject(hit.transform.gameObject);
            CastRestLaserBeam(2, hit, hit.point, LightLauncher.position);
        }
        else
        {
            IgnoreRestBeam(1);
        }
    }

    private void CastFirstLaserBeamTwoDotFiveD()
    {
        Vector3 launchPosition = LightLauncher.position;

        Ray detect = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(detect, out RaycastHit detectHit))
        {
            Vector3 detectPosition = detectHit.point;
            Vector3 endPosition = new Vector3(detectPosition.x, launchPosition.y, detectPosition.z);

            Vector3 launchDirection = endPosition - launchPosition;

            Ray ray = new Ray(launchPosition, launchDirection);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                LightCreator.SetPosition(1, hit.point);
                LightEventManager.TriggerLightHitObject(hit.transform.gameObject);
                CastRestLaserBeam(2, hit, hit.point, LightLauncher.position);
            }
            else
            {
                IgnoreRestBeam(1);
            }
        }
    }

    private void IgnoreRestBeam(int positionIndex)
    {
        if (positionIndex >= LightCreator.positionCount)
            return;

        LightCreator.SetPosition(positionIndex, LightCreator.GetPosition(positionIndex - 1));
        IgnoreRestBeam(positionIndex + 1);
    }

    private void CastRestLaserBeam(int positionIndex, RaycastHit hitInfo, Vector3 curPosition, Vector3 prePosition)
    {
        if (positionIndex >= LightCreator.positionCount)
            return;

        if (hitInfo.transform.tag != "Reflect" && hitInfo.transform.tag != "Refract")
        {
            IgnoreRestBeam(positionIndex);
            return;
        }

        if (hitInfo.transform.tag == "Reflect")
        {
            Vector3 inDir = curPosition - prePosition;
            Vector3 reflectDir = Vector3.Reflect(inDir, hitInfo.normal);

            Ray ray = new Ray(hitInfo.point, reflectDir);
            if (Physics.Raycast(ray, out RaycastHit reflectHit))
            {
                LightCreator.SetPosition(positionIndex, reflectHit.point);
                LightEventManager.TriggerLightHitObject(reflectHit.transform.gameObject);
                CastRestLaserBeam(positionIndex + 1, reflectHit, reflectHit.point, curPosition);
            }
            else
            {
                IgnoreRestBeam(positionIndex);
            }
        }

        if (hitInfo.transform.tag == "Refract")
        {
            RefractObject ro = hitInfo.transform.gameObject.GetComponent<RefractObject>();
            if (ro == null)
                Debug.LogError("Please add RefractObject component to gameObject with tag \"Refract\".");

            Vector3 inDir = curPosition - prePosition;
            Vector3 refractDir = RefractLighting.Refract(inDir, hitInfo.normal, ro.RefractFactor / 1.0f);

            Ray ray = new Ray(hitInfo.point, refractDir);
            if (Physics.Raycast(ray, out RaycastHit refractHit))
            {
                LightCreator.SetPosition(positionIndex, refractHit.point);
                LightEventManager.TriggerLightHitObject(refractHit.transform.gameObject);
                CastRestLaserBeam(positionIndex + 1, refractHit, refractHit.point, curPosition);
            }
            else
            {
                IgnoreRestBeam(positionIndex);
            }
        }
    }

    private void CastSingleLaserBeam()
    {

    }
}