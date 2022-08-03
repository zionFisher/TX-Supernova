using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamController : MonoBehaviour
{
    public float Speed = 10;

    public LineRenderer LineRenderer;

    public Color[] Colors;

    public float Intensity;

    public float InnerWeight = 0.6f;

    public float curFrequency = 385;
    private float lower = 385;
    private float higher = 750;
    private float[] FrequencyRange = new float[] { 384, 384, 433, 493, 512, 565, 635, 705, 751, 751 };

    private void Start()
    {
        Utility.CheckUnassignedVar<LineRenderer>(LineRenderer);

        InputEventManager.EventLaserBeamWaveBand += UpdateFrequency;
    }

    private void OnDestroy()
    {
        InputEventManager.EventLaserBeamWaveBand -= UpdateFrequency;
    }

    private void UpdateFrequency(float offset)
    {
        if (CameraManager.Instance.PlayerCameraMode != CameraMode.FPS3D && CameraManager.Instance.PlayerCameraMode != CameraMode.FPS2Dot5D)
            return;

        if (Mathf.Abs(offset) < 0.01)
            return;

        curFrequency += offset * Speed;

        if (curFrequency > higher)
            curFrequency = higher;
        if (curFrequency < lower)
            curFrequency = lower;

        float innerLower = 0, innerHigher = 0;
        float outerLower = 0, outerHigher = 0;
        int index = 1;
        for (int i = 2; i < FrequencyRange.Length; i++)
        {
            if (curFrequency < FrequencyRange[i])
            {
                innerLower = FrequencyRange[i - 1];
                innerHigher = FrequencyRange[i];
                outerLower = FrequencyRange[i - 2];
                outerHigher = FrequencyRange[i + 1];
                index = i;
                break;
            }
        }

        float innerPercentage = (curFrequency - innerLower) / (innerHigher - innerLower);
        float outerPercentage = (curFrequency - outerLower) / (outerHigher - outerLower);
        Color innerColor = Colors[index] * innerPercentage + Colors[index - 1] * (1 - innerPercentage);
        Color outerColor = Colors[index + 1] * outerPercentage + Colors[index - 2] * (1 - outerPercentage);

        Color resColor = InnerWeight * innerColor + (1 - InnerWeight) * outerColor;

        LineRenderer.material.SetColor("_Color", resColor * Mathf.Pow(2, Intensity));
    }
}