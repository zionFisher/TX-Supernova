using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamController : MonoBehaviour
{
    public float Speed = 10;

    public LineRenderer LineRenderer;

    public Color[] Colors;

    public float Intensity;

    public float curFrequency = 385;
    private float lower = 385;
    private float higher = 750;
    private float[] FrequencyRange = new float[] { 384, 482, 503, 610, 659, 751 };

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
        if (Mathf.Abs(offset) < 0.01)
            return;

        curFrequency += offset * Speed;

        if (curFrequency > higher)
            curFrequency = higher;
        if (curFrequency < lower)
            curFrequency = lower;

        float curLower = 0, curHigher = 0;
        int index = 0;
        for (int i = 1; i < FrequencyRange.Length; i++)
        {
            if (curFrequency < FrequencyRange[i])
            {
                curLower = FrequencyRange[i - 1];
                curHigher = FrequencyRange[i];
                index = i;
                break;
            }
        }

        float percentage = (curFrequency - curLower) / (curHigher - curLower);
        Color resColor = Colors[index] * percentage + Colors[index - 1] * (1 - percentage);

        LineRenderer.material.SetColor("_Color", resColor * Mathf.Pow(2, Intensity));
    }
}